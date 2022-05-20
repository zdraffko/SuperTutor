import { HubConnectionBuilder, HubConnectionState, IHttpConnectionOptions, LogLevel } from "@microsoft/signalr";
import { useCallback, useEffect, useState } from "react";
import authTokenStorage from "utils/authTokenStorage";

const useClassroomHub = () => {
    const [classroomHub] = useState(
        new HubConnectionBuilder()
            .withUrl("http://localhost:5004/hubs/classroom", {
                accessTokenFactory: () => authTokenStorage.get()
            } as IHttpConnectionOptions)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build()
    );

    const isHubConnected = useCallback(() => classroomHub && classroomHub.state === HubConnectionState.Connected, [classroomHub]);

    useEffect(() => {
        classroomHub.onclose(error => {
            console.log("SignalR: Closed connection to video conference hub");

            if (error) {
                console.log(error);
            }
        });

        classroomHub.on("log", logMessage => console.log(`Log from hub: ${logMessage}`));

        console.log("starting");
        classroomHub
            .start()
            .then(() => {
                if (isHubConnected()) {
                    console.log("SignalR: Connected to video conference hub");
                }
            })
            .catch(error => {
                console.error(`SignalR: Failed to connect to video conference hub ${error.toString()}`);
            });

        return () => {
            classroomHub.stop().then(() => console.log("Stopping connection"));
        };
    }, [classroomHub, isHubConnected]);

    return { classroomHub, isHubConnected };
};

export default useClassroomHub;
