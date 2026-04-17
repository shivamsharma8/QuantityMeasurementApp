version: '3.4'

services:
  auth-service:
    build:
      context: ./services/auth-service
    ports:
      - "5001:5001"
    environment:
      - ASPNETCORE_URLS=http://+:5001
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=QuantityDB_Auth;User Id=sa;Password=YourStrong@123;TrustServerCertificate=True;
    depends_on:
      - sqlserver
    networks:
      - quantity-network

  quantity-service:
    build:
      context: ./services/quantity-service
    ports:
      - "5002:5002"
    environment:
      - ASPNETCORE_URLS=http://+:5002
    networks:
      - quantity-network

  history-service:
    build:
      context: ./services/history-service
    ports:
      - "5003:5003"
    environment:
      - ASPNETCORE_URLS=http://+:5003
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=QuantityDB_History;User Id=sa;Password=YourStrong@123;TrustServerCertificate=True;
    depends_on:
      - sqlserver
    networks:
      - quantity-network

  gateway:
    build:
      context: ./services/gateway
    ports:
      - "5005:5005"
    environment:
      - ASPNETCORE_URLS=http://+:5005
      - ReverseProxy__Clusters__auth-cluster__Destinations__destination1__Address=http://auth-service:5001
      - ReverseProxy__Clusters__quantity-cluster__Destinations__destination1__Address=http://quantity-service:5002
      - ReverseProxy__Clusters__history-cluster__Destinations__destination1__Address=http://history-service:5003
    depends_on:
      - auth-service
      - quantity-service
      - history-service
    networks:
      - quantity-network

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=YourStrong@123
    ports:
      - "1434:1433"
    networks:
      - quantity-network

networks:
  quantity-network:
    driver: bridge
