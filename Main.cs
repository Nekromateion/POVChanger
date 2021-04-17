﻿using System;
using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace POVChanger
{
    public class Main : MelonMod
    {
        //HmdPivot
        private Camera _MyCam;
        private Transform _Neck;
        private Vector3 _OriginalScale;
        private Camera _PlayerCam;

        public override void VRChat_OnUiManagerInit()
        {
            _MyCam = Camera.main;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                foreach (var oodListener in Object.FindObjectsOfType<AudioListener>())
                    Console.WriteLine(oodListener.name);

                Console.WriteLine(Camera.main.name);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    _MyCam.enabled = false;
                    GameObject.Find(
                            "_Application/TrackingVolume/TrackingSteam(Clone)/SteamCamera/[CameraRig]/Neck/Camera (head)/Camera (ears)")
                        .GetComponent<AudioListener>().enabled = false;
                    var ply = QuickMenu.prop_QuickMenu_0.field_Private_Player_0;

                    if (ply.transform.Find("ForwardDirection/Avatar").GetComponent<Animator>()
                        .GetBoneTransform(HumanBodyBones.Head).FindChild("HmdPivot").GetComponent<Camera>())
                    {
                        _PlayerCam = ply.transform.Find("ForwardDirection/Avatar").GetComponent<Animator>()
                            .GetBoneTransform(HumanBodyBones.Head).FindChild("HmdPivot").GetComponent<Camera>();
                        _PlayerCam.GetComponent<AudioListener>().enabled = true;
                        _PlayerCam.enabled = true;
                        _Neck = ply.transform.Find("ForwardDirection/Avatar").GetComponent<Animator>()
                            .GetBoneTransform(HumanBodyBones.Neck);
                        _OriginalScale = _Neck.localScale;
                        _Neck.localScale = new Vector3(0, _OriginalScale.y, _OriginalScale.z);
                    }
                    else
                    {
                        _PlayerCam = ply.transform.Find("ForwardDirection/Avatar").GetComponent<Animator>()
                            .GetBoneTransform(HumanBodyBones.Head).FindChild("HmdPivot").gameObject
                            .AddComponent<Camera>();
                        _PlayerCam.gameObject.AddComponent<AudioListener>();
                        _PlayerCam.fieldOfView = 90;
                        _PlayerCam.nearClipPlane = 0.1f;
                        _PlayerCam.enabled = true;
                        _Neck = ply.transform.Find("ForwardDirection/Avatar").GetComponent<Animator>()
                            .GetBoneTransform(HumanBodyBones.Neck);
                        _OriginalScale = _Neck.localScale;
                        _Neck.localScale = new Vector3(0, _OriginalScale.y, _OriginalScale.z);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    _MyCam.enabled = true;
                    GameObject.Find(
                            "_Application/TrackingVolume/TrackingSteam(Clone)/SteamCamera/[CameraRig]/Neck/Camera (head)/Camera (ears)")
                        .GetComponent<AudioListener>().enabled = true;
                    _Neck.localScale = _OriginalScale;
                    _PlayerCam.enabled = false;
                    _PlayerCam.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
    }
}