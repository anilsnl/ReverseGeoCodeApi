# Reverse GeoCode Api
The API use latitude and longitude with reverse geocoding to determine user's location

## The using technologies & libraries I used are the following...
1. .NET 5
2. MongoDb - with official .NET Driver
3. CQRS
4. Swagger
5. MediatR
6. FluentValidation
7. AutoMapper
8. Docker
9. Xunit

The using data set is geojson format. You're able to use any data that this format. You can obtain this data from OpenStreetMap dataset as free.

! You can find sample document in doc directory.
!! You can run the following command to create single MongoDb.

```bash
docker run -d --name dev-mongo -e "MONGO_INITDB_ROOT_PASSWORD=52" -p 27017:27017 mongo 
```
