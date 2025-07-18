﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

namespace Abelkhan
{
    public class WSChannel : Abelkhan.Ichannel
    {
        public event Action<WSChannel> onDisconnect;
        public event Action<WSChannel> Disconnect;

        public ChannelOnRecv _channel_onrecv;

        public WSChannel(WebSocket _s)
        {
            s = _s;
            _channel_onrecv = new ChannelOnRecv(this);

            s.OnMessage += (sender, e) => {
                Debug.Log($"OnMessage {e.RawData}");
                onRead(e.RawData);
            };
        }

        public void disconnect()
        {
            s.CloseAsync();

            Disconnect?.Invoke(this);
        }

        public bool is_xor_key_crypt()
        {
            return false;
        }

        public void normal_send_crypt(byte[] data)
        {
        }

        private void onRead(byte[] e)
        {
            try
            {
                _channel_onrecv.on_recv(e);
            }
            catch (System.ObjectDisposedException)
            {
                onDisconnect?.Invoke(this);

            }
            catch (System.Net.Sockets.SocketException)
            {
                onDisconnect?.Invoke(this);
            }
        }

        public void send(byte[] data)
        {
            s.SendAsync(data);
        }

        public WebSocket s;

    }
}

