# kontur-server-test-task

This is my old test task for c-sharp developer position. Honestly, now it seems to me a bit ugly =)

The goal was to create 2 applications:
  * Server capable to return suggests like autocomplete, list of words passed in file on startup
  * Client to this server
  
I used [trie data structure](https://en.wikipedia.org/wiki/Trie).
Trie realization was picked from [VDS.common](https://www.nuget.org/packages/VDS.Common/) and modified to store limited amount of data in nodes for better perfomance and memory consumption. 
