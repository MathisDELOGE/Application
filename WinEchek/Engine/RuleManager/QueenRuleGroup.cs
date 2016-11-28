﻿using System;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class QueenRuleGroup : RuleGroup
    {
        public QueenRuleGroup()
        {
            Rules.Add(new QueenMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
        }

        public override bool Handle(Move move)
        {
            if (move.Piece.Type != Type.Queen)
            {
                if (Next != null)
                {
                    return Next.Handle(move);
                }
                throw new Exception("NOBODY TREATS THIS PIECE !!! " + move.Piece);
            }
            return Rules.All(rule => rule.IsMoveValid(move));
        }
    }
}