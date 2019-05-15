FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["EmployeesWebNetCore.csproj", "."]
RUN dotnet restore "EmployeesWebNetCore.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "EmployeesWebNetCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmployeesWebNetCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

VOLUME [ "/app/data" ]
ENV DEFAULT=/app/data/employees.txt
ARG filepath={DEFAULT}

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmployeesWebNetCore.dll"]
CMD ["filepath=/app/data/employees.txt"]