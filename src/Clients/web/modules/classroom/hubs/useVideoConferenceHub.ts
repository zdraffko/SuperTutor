import { HubConnectionBuilder, HubConnectionState, IHttpConnectionOptions, LogLevel } from "@microsoft/signalr";
import { useCallback, useEffect, useState } from "react";
import authTokenStorage from "utils/authTokenStorage";

const useVideoConferenceHub = () => {
    const [hubConnection] = useState(
        new HubConnectionBuilder()
            .withUrl("http://localhost:5004/hubs/videoconference", {
                accessTokenFactory: () => authTokenStorage.get()
            } as IHttpConnectionOptions)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build()
    );

    const isHubConnected = useCallback(() => hubConnection && hubConnection.state === HubConnectionState.Connected, [hubConnection]);

    useEffect(() => {
        hubConnection.onclose(error => {
            console.log("SignalR: Clsoed connection to video conference hub");

            if (error) {
                console.log(error);
            }
        });

        hubConnection.on("log", logMessage => console.log(`Log from hub: ${logMessage}`));

        console.log("starting");
        hubConnection
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
            hubConnection.stop().then(() => console.log("Stopping connection"));
        };
    }, [hubConnection, isHubConnected]);

    return { hubConnection, isHubConnected };
};

export default useVideoConferenceHub;
