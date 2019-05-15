FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["EmployeesWebNetCore/EmployeesWebNetCore.csproj", "EmployeesWebNetCore/"]
RUN dotnet restore "EmployeesWebNetCore/EmployeesWebNetCore.csproj"
COPY . .
WORKDIR "/src/EmployeesWebNetCore"
RUN dotnet build "EmployeesWebNetCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmployeesWebNetCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
ENV DATA=app/data/Employees.txt
ARG filepath={DATA}
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmployeesWebNetCore.dll"]
CMD ["{filepath}" ]