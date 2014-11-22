using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Lite;
using InputManager;

namespace TeamSync
{
    enum OpCodeEnum : byte
    {
        Join = 255,
        Leave = 254,
        RaiseEvent = 253,
        SetProperties = 252,
        GetProperties = 251
    }

    enum EvCodeEnum : byte
    {
        KeyPressed = 101,
        KeyReleased = 102,
        Join = 255,
        Leave = 254,
        PropertiesChanged = 253
    }

    public partial class Form1 : Form, IPhotonPeerListener
    {
        PhotonPeer peer;
        List<int> ignorePress, ignoreRelease;
        List<Keys> ignoredKeys;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ignorePress = new List<int>();
            ignoreRelease = new List<int>();
            peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            ignoredKeys = new List<Keys> {Keys.LWin, Keys.RWin, Keys.LControlKey, Keys.RControlKey, Keys.Alt, Keys.PrintScreen, Keys.Escape, Keys.P, Keys.C, Keys.Tab};

        }

        void RemoteKeyDown(int keycode)
        {
            Dictionary<byte, object> opParams = new Dictionary<byte, object>();
            Hashtable evData = new Hashtable();
            evData[(byte)1] = keycode;
            opParams[LiteOpKey.Data] = evData;
            opParams[LiteOpKey.Code] = (byte)EvCodeEnum.KeyPressed;
            peer.OpCustom((byte)LiteOpCode.RaiseEvent, opParams, true);
        }


        void RemoteKeyUp(int keycode)
        {
            Dictionary<byte, object> opParams = new Dictionary<byte, object>();
            Hashtable evData = new Hashtable();
            evData[(byte)1] = keycode;
            opParams[LiteOpKey.Data] = evData;
            opParams[LiteOpKey.Code] = (byte)EvCodeEnum.KeyReleased;
            peer.OpCustom((byte)LiteOpCode.RaiseEvent, opParams, true);
        }

        void SendKeyDown(int keycode)
        {
            Keyboard.KeyDown((Keys)keycode);
        }

        void SendKeyUp(int keycode)
        {
            Keyboard.KeyUp((Keys)keycode);
        }


        public void DebugReturn(DebugLevel level, string message)
        {
        }

        public void OnEvent(EventData eventData)
        {
            switch (eventData.Code)
            {
                case (byte)EvCodeEnum.KeyPressed:
                    Hashtable evData0 = (Hashtable)eventData.Parameters[LiteEventKey.Data];
                    ignorePress.Add((int)evData0[(byte)1]);
                    SendKeyDown((int)evData0[(byte)1]);
                    Debug.Items.Add("Received Key Pressed: " + evData0[(byte)1].ToString());
                    break;
                case (byte)EvCodeEnum.KeyReleased:
                    Hashtable evData1 = (Hashtable)eventData.Parameters[LiteEventKey.Data];
                    ignoreRelease.Add((int)evData1[(byte)1]);
                    SendKeyUp((int)evData1[(byte)1]);
                    Debug.Items.Add("Received Key Released: " + evData1[(byte)1].ToString());
                    break;
            }
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Debug.Items.Add("Operation Response: " + operationResponse.DebugMessage + "(" + operationResponse.OperationCode + ")");
            switch (operationResponse.OperationCode)
            {
                case LiteOpCode.Join:
                    connectbutton.Enabled = Room.Enabled = serverAddress.Enabled = false;
                    int myActorNr = (int)operationResponse.Parameters[LiteOpKey.ActorNr];
                    Debug.Items.Add("My Player ID is " + myActorNr);
                    break;
            }
        }

        public void OnStatusChanged(StatusCode statusCode)
        {

            switch (statusCode)
            {
                case StatusCode.Connect:
                    Dictionary<Byte, Object> opParams = new Dictionary<Byte, Object>();
                    opParams[(byte)LiteOpKey.GameId] = Room.Text;
                    peer.OpCustom((byte)LiteOpCode.Join, opParams, true);
                    break;
                default:
                    connectbutton.Enabled = Room.Enabled = serverAddress.Enabled = true;
                    break;
            }

            Debug.Items.Add("Status Changed: " + statusCode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            peer.Service();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            peer.Disconnect();
        }

        private void connectbutton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Warning: Connecting now will start broadcasting keypresses to other players in the room. If you have to type a password or other sensitive information, do that before connecting.", "Security Warning", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (peer.Connect(serverAddress.Text, "KeyboardSync"))
                {
                    timer1.Start();
                }
                KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler(keyHook_KeyDown);
                KeyboardHook.KeyUp += new KeyboardHook.KeyUpEventHandler(keyHook_KeyUp);

                KeyboardHook.InstallHook();
            }
        }

        void keyHook_KeyUp(int vkCode)
        {
            if (ignoredKeys.Contains((Keys)vkCode)) return;

            if (ignoreRelease.Contains(vkCode))
            {
                ignoreRelease.Remove(vkCode);
            }
            else
            {
                RemoteKeyUp(vkCode);
            }
        }

        void keyHook_KeyDown(int vkCode)
        {
            if (ignoredKeys.Contains((Keys)vkCode)) return;

            if (ignorePress.Contains(vkCode))
            {
                ignorePress.Remove(vkCode);
            }
            else
            {
                RemoteKeyDown(vkCode);
            }
        }

    }

}
