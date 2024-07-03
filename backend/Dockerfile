# Stage 1: Compile and Build C# codebase

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Install Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef
RUN dotnet tool install --global SwashBuckle.AspNetCore.Cli

COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out


# Stage 2: Build backend with ASP .NET

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the desired port
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001

ENTRYPOINT ["dotnet", "SkyExplorer.dll", "migrate"]