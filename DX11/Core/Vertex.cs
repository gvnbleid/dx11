﻿using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.D3DCompiler;

namespace Core {
    using System;

    using Core.FX;

    using SlimDX.DXGI;
    using SlimDX.Direct3D11;

    using Device = SlimDX.Direct3D11.Device;

    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPC {
        public Vector3 Pos;
        public Color4 Color;

        public VertexPC(Vector3 pos, Color color) {
            Pos = pos;
            Color = color;
        }

        public VertexPC(Vector3 pos, Color4 color) {
            Pos = pos;
            Color = color;
        }

        public static readonly int Stride = Marshal.SizeOf(typeof(VertexPC));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPN {
        public Vector3 Position;
        public Vector3 Normal;

        public VertexPN(Vector3 position, Vector3 normal) {
            Position = position;
            Normal = normal;
        }

        public static readonly int Stride = Marshal.SizeOf(typeof(VertexPN));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Basic32 {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 Tex;

        public Basic32(Vector3 position, Vector3 normal, Vector2 texC) {
            Position = position;
            Normal = normal;
            Tex = texC;
        }

        public static readonly int Stride = Marshal.SizeOf(typeof(Basic32));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TreePointSprite {
        public Vector3 Pos;
        public Vector2 Size;

        public static readonly int Stride = Marshal.SizeOf(typeof(TreePointSprite));
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PosNormalTexTan {
        public Vector3 Pos;
        public Vector3 Normal;
        public Vector2 Tex;
        public Vector3 Tan;
        public static readonly int Stride = Marshal.SizeOf(typeof(PosNormalTexTan));

        public PosNormalTexTan(Vector3 position, Vector3 normal, Vector2 texC, Vector3 tangentU) {
            Pos = position;
            Normal = normal;
            Tex = texC;
            Tan = tangentU;
        }
    }

    public static class InputLayoutDescriptions {
        public static readonly InputElement[] Pos = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0 ), 
        };
        public static readonly InputElement[] PosNormal = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
            new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0, InputClassification.PerVertexData, 0)
        };
        public static readonly InputElement[] Basic32 = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
            new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0, InputClassification.PerVertexData, 0), 
            new InputElement("TEXCOORD", 0, Format.R32G32_Float, 24, 0, InputClassification.PerVertexData, 0)
        };
        public static readonly InputElement[] TreePointSprite = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0 ),
            new InputElement("SIZE", 0, Format.R32G32_Float, 12,0,InputClassification.PerVertexData, 0) 
        };
        public static readonly InputElement[] InstancedBasic32 = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
            new InputElement("NORMAL", 0, Format.R32G32B32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0), 
            new InputElement("TEXCOORD", 0, Format.R32G32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0),
            new InputElement("WORLD", 0, Format.R32G32B32A32_Float, 0, 1, InputClassification.PerInstanceData, 1 ), 
            new InputElement("WORLD", 1, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1 ),
            new InputElement("WORLD", 2, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1 ),
            new InputElement("WORLD", 3, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1 ),
            new InputElement("COLOR", 0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1 )
        };
        public static readonly InputElement[] PosNormalTexTan = {
            new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
            new InputElement("NORMAL", 0, Format.R32G32B32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0), 
            new InputElement("TEXCOORD", 0, Format.R32G32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0),
            new InputElement("TANGENT", 0, Format.R32G32B32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData,0 ) 
        };
    }
    public static class InputLayouts {
        public static void InitAll(Device device) {
            
            try {
                var passDesc = Effects.BasicFX.Light1Tech.GetPassByIndex(0).Description;
                PosNormal = new InputLayout(device, passDesc.Signature, InputLayoutDescriptions.PosNormal);
            } catch (Exception dex) {
                Console.WriteLine(dex.Message + dex.StackTrace);
                PosNormal = null;
            }
            try {
                var passDesc = Effects.BasicFX.Light1Tech.GetPassByIndex(0).Description;
                Basic32 = new InputLayout(device, passDesc.Signature, InputLayoutDescriptions.Basic32);
            } catch (Exception dex) {
                Console.WriteLine(dex.Message + dex.StackTrace);
                Basic32 = null;
            }
            try {
                var shaderSignature = Effects.InstancedBasicFX.Light1Tech.GetPassByIndex(0).Description.Signature;
                InstancedBasic32 = new InputLayout(device, shaderSignature, InputLayoutDescriptions.InstancedBasic32);
            } catch (Exception dex) {
                Console.WriteLine(dex.Message + dex.StackTrace);
                InstancedBasic32 = null;
            }
            try {
                var passDesc = Effects.TreeSpriteFX.Light3Tech.GetPassByIndex(0).Description;
                TreePointSprite = new InputLayout(device, passDesc.Signature, InputLayoutDescriptions.TreePointSprite);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + ex.StackTrace);
                TreePointSprite = null;
            }
            try {
                var passDesc = Effects.SkyFX.SkyTech.GetPassByIndex(0).Description;
                Pos = new InputLayout(device, passDesc.Signature, InputLayoutDescriptions.Pos);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + ex.StackTrace);
                Pos = null;
            }
            try {
                var passDesc = Effects.NormalMapFX.Light1Tech.GetPassByIndex(0).Description;
                PosNormalTexTan = new InputLayout(device, passDesc.Signature, InputLayoutDescriptions.PosNormalTexTan);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + ex.StackTrace);
                PosNormalTexTan = null;
            }
            
        }
        public static void DestroyAll() {
            Util.ReleaseCom(ref Pos);
            Util.ReleaseCom(ref PosNormal);
            Util.ReleaseCom(ref Basic32);
            Util.ReleaseCom(ref TreePointSprite);
            Util.ReleaseCom(ref InstancedBasic32);
            Util.ReleaseCom(ref PosNormalTexTan);
        }

        public static InputLayout PosNormal;
        public static InputLayout Basic32;
        public static InputLayout TreePointSprite;
        public static InputLayout InstancedBasic32;
        public static InputLayout Pos;
        public static InputLayout PosNormalTexTan;
    }
}