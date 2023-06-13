#Use Case 1
##Description
This Countries API provides filtering, sorting and paging features for the REST Countries public API.
Features:
- fetch the wole list of countries;
- fetch the results filtered by name and population;
- fetch the results sorted and paged.

##Running locally
To run the application locally switch to the UC1 folder where UC1.sln is located and run the command from the console:
```
dotnet run
```

##Example URLs
- https://<hostname:port>/api/countries?searchString=land
- https://<hostname:port>/api/countries?searchString=land&sortDirection=ascend&pageSize=15
- https://<hostname:port>/api/countries?population=50
- https://<hostname:port>/api/countries?population=5&sortDirection=ascend&pageSize=15
- https://<hostname:port>/api/countries?sortDirection=ascend&pageSize=15
- https://<hostname:port>/api/countries?sortDirection=descend&pageSize=15
- https://<hostname:port>/api/countries?searchString=land&population=5&sortDirection=ascend&pageSize=30
- https://<hostname:port>/api/countries?searchString=land&population=5&sortDirection=desc&pageSize=30
- https://<hostname:port>/api/countries?searchString=land&population=10&sortDirection=ascend&pageSize=20
- https://<hostname:port>/api/countries?searchString=land&population=10&sortDirection=desc&pageSize=20