export interface PeerDataChannelMessage<TPayload> {
    type: string;
    payload: TPayload;
}
