// SPDX-FileCopyrightText: 2026 CrimeMoot <169259387+crimemoot@users.noreply.github.com>
// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Power.Components;
using Content.Server.Power.EntitySystems;
using Content.Shared.Audio.Jukebox;
using Content.Shared.Power;
using Robust.Server.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Components;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Server.Audio.Jukebox;

public sealed partial class JukeboxSystem : SharedJukeboxSystem
{
    [Dependency] private IPrototypeManager _protoManager = default!;
    [Dependency] private AppearanceSystem _appearanceSystem = default!;
    // Orion-Start
    [Dependency] private IGameTiming _gameTiming = default!;
    // Orion-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<JukeboxComponent, JukeboxSelectedMessage>(OnJukeboxSelected);
        SubscribeLocalEvent<JukeboxComponent, JukeboxPlayingMessage>(OnJukeboxPlay);
        SubscribeLocalEvent<JukeboxComponent, JukeboxPauseMessage>(OnJukeboxPause);
        SubscribeLocalEvent<JukeboxComponent, JukeboxStopMessage>(OnJukeboxStop);
        SubscribeLocalEvent<JukeboxComponent, JukeboxSetTimeMessage>(OnJukeboxSetTime);
        // Orion-Start
        SubscribeLocalEvent<JukeboxComponent, JukeboxSetVolumeMessage>(OnJukeboxSetVolume);
        SubscribeLocalEvent<JukeboxComponent, JukeboxToggleLoopMessage>(OnJukeboxToggleLoop);
        // Orion-End
        SubscribeLocalEvent<JukeboxComponent, ComponentInit>(OnComponentInit);
        SubscribeLocalEvent<JukeboxComponent, ComponentShutdown>(OnComponentShutdown);

        SubscribeLocalEvent<JukeboxComponent, PowerChangedEvent>(OnPowerChanged);
    }

    private void OnComponentInit(Entity<JukeboxComponent> ent, ref ComponentInit args)
    {
        if (HasComp<ApcPowerReceiverComponent>(ent))
        {
            TryUpdateVisualState(ent.AsNullable());
        }
    }

    private void OnJukeboxPlay(Entity<JukeboxComponent> ent, ref JukeboxPlayingMessage args)
    {
        // Orion-Edit-Start
        if (!TryPlay(ent.AsNullable()))
            return;

        ent.Comp.PlaybackStartTime = _gameTiming.CurTime;
        Dirty(ent);
        // Orion-Edit-End
    }

    private void OnJukeboxPause(Entity<JukeboxComponent> ent, ref JukeboxPauseMessage args)
    {
        // Orion-Start
        UpdatePlaybackOffset(ent);
        // Orion-End

        Pause(ent.AsNullable());

        // Orion-Start
        Dirty(ent);
        // Orion-End
    }

    private void OnJukeboxSetTime(Entity<JukeboxComponent> ent, ref JukeboxSetTimeMessage args)
    {
        if (TryComp(args.Actor, out ActorComponent? actorComp))
        {
            var offset = actorComp.PlayerSession.Channel.Ping * 1.5f / 1000f;
            // Orion-Start
            var playbackPosition = args.SongTime + offset;
            // Orion-End
            SetTime(ent.AsNullable(), playbackPosition); // Orion-Edit
            // Orion-Start
            ent.Comp.CurrentPlaybackOffset = playbackPosition;
            ent.Comp.PlaybackStartTime = _gameTiming.CurTime;
            Dirty(ent);
            // Orion-End
        }
    }

    // Orion-Start
    private void OnJukeboxSetVolume(Entity<JukeboxComponent> ent, ref JukeboxSetVolumeMessage args)
    {
        ent.Comp.Volume = Math.Clamp(args.Volume, 0f, 100f);
        if (TryComp<AudioComponent>(ent.Comp.AudioStream, out _))
            Audio.SetVolume(ent.Comp.AudioStream, SharedJukeboxSystem.MapVolume(ent.Comp.Volume));

        Dirty(ent);
    }

    private void OnJukeboxToggleLoop(Entity<JukeboxComponent> ent, ref JukeboxToggleLoopMessage args)
    {
        ent.Comp.LoopEnabled = !ent.Comp.LoopEnabled;
        Dirty(ent);
    }
    // Orion-End

    private void OnPowerChanged(Entity<JukeboxComponent> entity, ref PowerChangedEvent args)
    {
        TryUpdateVisualState(entity.AsNullable());

        if (!this.IsPowered(entity.Owner, EntityManager))
        {
            Stop(entity.AsNullable());
        }
    }

    private void OnJukeboxStop(Entity<JukeboxComponent> entity, ref JukeboxStopMessage args)
    {
        Stop(entity.AsNullable());
    }

    private void OnJukeboxSelected(EntityUid uid, JukeboxComponent component, JukeboxSelectedMessage args)
    {
        SetSelectedTrack((uid, component), args.SongId);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<JukeboxComponent>();
        while (query.MoveNext(out var uid, out var comp))
        {
            // Orion-Start
            if (comp.AudioStream is { } stream && TerminatingOrDeleted(stream))
            {
                comp.AudioStream = null;
                comp.CurrentPlaybackOffset = 0f;
                comp.PlaybackStartTime = null;
                Dirty(uid, comp);
                continue;
            }
            // Orion-End

            if (comp.Selecting)
            {
                comp.SelectAccumulator += frameTime;
                if (comp.SelectAccumulator >= 0.5f)
                {
                    comp.SelectAccumulator = 0f;
                    comp.Selecting = false;

                    TryUpdateVisualState((uid, comp));
                }
            }

            // Orion-Start
            if (comp.PlaybackStartTime == null || !TryComp(comp.AudioStream, out AudioComponent? audio))
                continue;

            var position = comp.CurrentPlaybackOffset + (float) (_gameTiming.CurTime - comp.PlaybackStartTime.Value).TotalSeconds;
            if (position < Audio.GetAudioLength(audio.FileName).TotalSeconds)
                continue;

            if (comp.LoopEnabled)
            {
                comp.CurrentPlaybackOffset = 0f;
                comp.PlaybackStartTime = _gameTiming.CurTime;
                Audio.SetPlaybackPosition(comp.AudioStream, 0f);
                Audio.SetState(comp.AudioStream, AudioState.Playing);
            }
            else
            {
                comp.AudioStream = Audio.Stop(comp.AudioStream);
                comp.CurrentPlaybackOffset = 0f;
                comp.PlaybackStartTime = null;
            }

            Dirty(uid, comp);
            // Orion-End
        }
    }

    private void OnComponentShutdown(Entity<JukeboxComponent> ent, ref ComponentShutdown args)
    {
        ent.Comp.AudioStream = Audio.Stop(ent.Comp.AudioStream);
    }

    private void DirectSetVisualState(EntityUid uid, JukeboxVisualState state)
    {
        _appearanceSystem.SetData(uid, JukeboxVisuals.VisualState, state);
    }

    private void TryUpdateVisualState(Entity<JukeboxComponent?> ent)
    {
        if (!Resolve(ent, ref ent.Comp))
            return;

        var finalState = JukeboxVisualState.On;

        if (!this.IsPowered(ent, EntityManager))
        {
            finalState = JukeboxVisualState.Off;
        }

        _appearanceSystem.SetData(ent, JukeboxVisuals.VisualState, finalState);
    }

    /// <summary>
    /// Set the selected track of the jukebox to the specified prototype.
    /// </summary>
    public void SetSelectedTrack(Entity<JukeboxComponent?> ent, ProtoId<JukeboxPrototype> track)
    {
        if (!Resolve(ent, ref ent.Comp))
            return;

        if (!Audio.IsPlaying(ent.Comp.AudioStream))
        {
            ent.Comp.SelectedSongId = track;
            DirectSetVisualState(ent, JukeboxVisualState.Select);
            ent.Comp.Selecting = true;
            ent.Comp.AudioStream = Audio.Stop(ent.Comp.AudioStream);
            // Orion-Start
            ent.Comp.CurrentPlaybackOffset = 0f;
            ent.Comp.PlaybackStartTime = null;
            // Orion-End
            Dirty(ent);
        }
    }

    /// <summary>
    /// Attempts to play the jukebox's current selected track.
    /// </summary>
    /// <returns>false if no track is selected or the track prototype cannot be found, otherwise true.</returns>
    public bool TryPlay(Entity<JukeboxComponent?> ent)
    {
        if (!Resolve(ent, ref ent.Comp))
            return false;

        if (Exists(ent.Comp.AudioStream))
        {
            Audio.SetState(ent.Comp.AudioStream, AudioState.Playing);
        }
        else
        {
            if (string.IsNullOrEmpty(ent.Comp.SelectedSongId) ||
                !_protoManager.Resolve(ent.Comp.SelectedSongId, out var jukeboxProto))
            {
                return false;
            }

            // Orion-Edit-Start
            var audioParams = AudioParams.Default
                .WithMaxDistance(10f)
                .WithVolume(SharedJukeboxSystem.MapVolume(ent.Comp.Volume))
                .WithPlayOffset(ent.Comp.CurrentPlaybackOffset);
            ent.Comp.AudioStream = Audio.PlayPvs(jukeboxProto.Path, ent, audioParams)?.Entity;
            // Orion-Edit-End
            Dirty(ent);
        }
        return true;
    }

    /// <summary>
    /// Stops any track that may currently be playing.
    /// </summary>
    public void Stop(Entity<JukeboxComponent?> entity)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return;

        // Orion-Edit-Start
        if (entity.Comp.AudioStream is { } stream && !TerminatingOrDeleted(stream))
            Audio.SetState(stream, AudioState.Stopped);
        // Orion-Edit-End

        // Orion-Start
        entity.Comp.AudioStream = null;
        entity.Comp.CurrentPlaybackOffset = 0f;
        entity.Comp.PlaybackStartTime = null;
        Dirty(entity);
        // Orion-End
    }

    // Orion-Start
    private void UpdatePlaybackOffset(Entity<JukeboxComponent> ent)
    {
        if (ent.Comp.PlaybackStartTime == null)
            return;

        ent.Comp.CurrentPlaybackOffset += (float) (_gameTiming.CurTime - ent.Comp.PlaybackStartTime.Value).TotalSeconds;
        ent.Comp.PlaybackStartTime = null;
    }
    // Orion-End

    /// <summary>
    /// Pauses any track that may currently be playing.
    /// </summary>
    public void Pause(Entity<JukeboxComponent?> entity)
    {
        if (!Resolve(entity, ref entity.Comp, logMissing: false))
            return;

        Audio.SetState(entity.Comp.AudioStream, AudioState.Paused);
    }

    /// <summary>
    /// Sets the playback position within the current audio track.
    /// </summary>
    /// <remarks>
    /// If setting based on user input, you may need to compensate for the player's ping.
    /// </remarks>
    public void SetTime(Entity<JukeboxComponent?> entity, float songTime)
    {
        if (!Resolve(entity, ref entity.Comp))
            return;

        Audio.SetPlaybackPosition(entity.Comp.AudioStream, songTime);
    }
}
