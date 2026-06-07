# Durable write pattern

A robust file update generally follows:

1. serialize and validate the complete new content;
2. create a unique temporary file in the destination filesystem;
3. write all bytes;
4. flush buffers when durability is required;
5. close the temporary file;
6. atomically replace or rename into place;
7. remove the temporary file on failure;
8. preserve or back up the previous valid file when recovery policy requires it.

Do not reuse a predictable shared temp name for concurrent writers.
