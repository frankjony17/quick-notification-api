FROM registry.devcompany.com.br/base-images/dotnet/sdk:3.1-alpine AS build
# FROM registry.devcompany.com.br/base-images/dotnet/sdk:3.1-buster AS build

# Copiar os arquivos para a imagem do container.
COPY . .

# Rodar o `dotnet publish`
RUN dotnet publish -c Release -o out Quick.Notify.Api.Command

# Gerar a imagem de produção
FROM registry.devcompany.com.br/base-images/dotnet/aspnet:3.1-alpine AS runtime
# FROM registry.devcompany.com.br/base-images/dotnet/aspnet:3.1-buster AS runtime

# Copiar o resultado do build.
COPY --from=build /app/out .

# Usar esta porta no container se for uma API. Caso contrário, delete estas linhas.
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Definir se aplicação deve executar no modo invariante à globalização (padrão na imagem base company)
# https://docs.microsoft.com/dotnet/core/run-time-config/globalization
# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Definir o entrypoint.
ENTRYPOINT ["dotnet", "Quick.Notify.Api.Command.dll"]
