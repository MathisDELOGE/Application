﻿using System;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class BishopRuleGroup : RuleGroup
    {
        public BishopRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new BishopMovementRule());
        }
        public override bool Handle(Move move)
        {
            if (move.Piece.Type != Type.Bishop)
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