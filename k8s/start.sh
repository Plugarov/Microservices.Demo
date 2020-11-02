
kubectl apply \
    -f ./namespace.yml \
    -f ./logserver.yml \
    -f ./sqlserver.yml \
    -f ./rabbitmq.yml \
    -f ./notificationservice.yml \
    -f ./workshopmanagementeventhandler.yml \
    -f ./customermanagementapi.yml \
    -f ./vehiclemanagementapi.yml \
    -f ./workshopmanagementapi.yml \
    -f ./webapp.yml