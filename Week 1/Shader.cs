using OpenTK.Graphics.OpenGL4;
using System;

namespace AGAIN
{
    public class Shader
    {
        public static string DefaultVS = @"#version 330 core
layout (location = 0) in vec2 inposition;
layout (location = 1) in vec4 incolor;  
layout (location = 2) in vec2 intexcord;
out vec4 vs_color;

void main(void)
{
 gl_Position = vec4(inposition,1,1);
 vs_color = incolor;
}";
        public static string DefaultFS = @"#version 330 core
in vec4 vs_color;
out vec4 color;

void main(void)
{
 color = vs_color;
}";
        public static int DefaultShader = CreateShader(DefaultVS, DefaultFS);
        public static int CreateShader(string vscode, string fscode)
        {
            int vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vscode);
            GL.CompileShader(vs);
            string InfoLog = GL.GetShaderInfoLog(vs);
            if (!string.IsNullOrEmpty(InfoLog))
            {
                throw new Exception("vs : " + InfoLog);
            }
            int fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fscode);
            GL.CompileShader(fs);
            InfoLog = GL.GetShaderInfoLog(fs);
            if (!string.IsNullOrEmpty(InfoLog))
            {
                throw new Exception("fs : " + InfoLog);
            }
            int Program = GL.CreateProgram();
            GL.AttachShader(Program, vs);
            GL.AttachShader(Program, fs);
            GL.LinkProgram(Program);
            InfoLog = GL.GetProgramInfoLog(Program);
            if (!string.IsNullOrEmpty(InfoLog))
            {
                throw new Exception("Program : " + InfoLog);
            }
            GL.DetachShader(Program, vs);
            GL.DetachShader(Program, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);
            return Program;
        }
    }
}