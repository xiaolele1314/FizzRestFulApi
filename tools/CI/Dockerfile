FROM byzan/dotnet/core/aspnet

ENV TZ=Asia/Shanghai

WORKDIR /app

EXPOSE 80

VOLUME ["/log"]

COPY ./ ./

ENTRYPOINT ["dotnet", "Fizz.SalesOrder.dll"]