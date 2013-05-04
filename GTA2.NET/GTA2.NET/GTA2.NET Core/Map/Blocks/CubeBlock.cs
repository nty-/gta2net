﻿// GTA2.NET
// 
// File: Cube.cs
// Created: 22.04.2013
// Created by: João Pires
// 
// Copyright (C) 2010-2013 Hiale
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies
// or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// Grand Theft Auto (GTA) is a registred trademark of Rockstar Games.
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hiale.GTA2NET.Core.Collision;

namespace Hiale.GTA2NET.Core.Map.Blocks
{
    //I know CubeBlock sounds stupid, but I wanted it to have consistent
    public class CubeBlock : Block
    {
        public CubeBlock() : base()
        {
            this.SlopeType = SlopeType.None;
        }

        public CubeBlock(BlockStructure blockStructure, Vector3 pos) : base(blockStructure, pos)
        {
            this.SlopeType = SlopeType.None;
        }        

        public override Block DeepCopy()
        {
            return new CubeBlock(BlockStructure,  Position);
        }

        public override Block DeepCopy(BlockStructure blockStructure, Vector3 pos)
        {
            return new CubeBlock(blockStructure, pos);
        }
        
        public override bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        public override void SetUpCube()
        {
            FaceCoordinates frontCoordinates;
            FaceCoordinates backCoordinates;
            PrepareCoordinates(out frontCoordinates, out backCoordinates);

            CreateFrontVertices(frontCoordinates);

            // Top face
            CreateTopVertices(frontCoordinates, backCoordinates);
            // Bottom face
            CreateBottomVertices(frontCoordinates, backCoordinates);
            // Left face
            CreateLeftVertices(frontCoordinates, backCoordinates, 0);
            // Right face
            CreateRightVertices( frontCoordinates, backCoordinates, 0);
        }

        public override void GetCollision(List<IObstacle> obstacles)
        {
            if (Left.Wall && Top.Wall && Right.Wall && Bottom.Wall)
            {
                obstacles.Add(new RectangleObstacle(new Vector2(Position.X, Position.Y), (int) Position.Z, 1, 1));
                return;
            }
            if (Left.Wall)
                obstacles.Add(GetDefaultLeftCollison());
            if (Top.Wall)
                obstacles.Add(GetDefaultTopCollison());
            if (Right.Wall)
                obstacles.Add(GetDefaultRightCollison());
            if (Bottom.Wall)
                obstacles.Add(GetDefaultBottomCollison());
        }
    }
}
