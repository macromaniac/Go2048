using System;
using System.Collections.Generic;
using UnityEngine;

public class AI {
	Game aiGame;
	PlayerColor aiPlayerColor;
	public AI(Board board, PlayState playState, PlayerColor playerColor) {
		aiGame = new Game(board.GetStateAsString(), playState);
		aiPlayerColor = playerColor;
	}
	public Direction FindBestMove() {
		if (aiGame.lastRealPlayState == PlayState.Draw
				|| aiGame.lastRealPlayState == PlayState.Loss
				|| aiGame.lastRealPlayState == PlayState.Win)
			return Direction.None;
		List<Direction> directions = aiGame.FindAvailableMoves(aiPlayerColor.ToInt());
		if (directions.Count == 0 )
			return Direction.None;
		return GetMoveFromMinimax();
	}

	//finds the board value relative to aiPlayerColor (backwards for some reason)
	private float FindBoardValue(Game game, PlayerColor playerColor, bool areMovesExausted) {

		if (game.lastRealPlayState == PlayState.Win)
			if (aiPlayerColor != playerColor)
				return float.MaxValue;
			else
				return float.MinValue;

		//running out of moves is a loss
		if (game.lastRealPlayState == PlayState.Loss || areMovesExausted)
			if (aiPlayerColor != playerColor)
				return float.MinValue;
			else
				return float.MaxValue;

		if (game.lastRealPlayState == PlayState.Draw)
			return 0f;

		float tileDiff = game.GetBoard().NumTilesOfColor(aiPlayerColor) - game.GetBoard().NumTilesOfColor(aiPlayerColor.Flip());
		return tileDiff;
	}

	Direction bestMove;
	int maxDepth = 5;
	private Direction GetMoveFromMinimax() {
		bestMove = Direction.None;
		float moveVal = Minimax(aiGame, maxDepth, float.MinValue, float.MaxValue, true, aiPlayerColor);
		Debug.Log(moveVal);
		return bestMove;
	}

	private float Minimax(Game aiGame, int depth, float alpha, float beta, bool isMaximizingPlayer, PlayerColor playerColor) {
		if (depth == 0 || aiGame.lastRealPlayState == PlayState.Draw
				|| aiGame.lastRealPlayState == PlayState.Loss
				|| aiGame.lastRealPlayState == PlayState.Win) {
			if (depth == maxDepth)
				Debug.Log("GameEnding at Highest State!");
			return FindBoardValue(aiGame, playerColor, false);
		}

		List<Direction> availableMoves = aiGame.FindAvailableMoves(playerColor.ToInt());
		if (availableMoves.Count == 0)
			return FindBoardValue(aiGame, playerColor, availableMoves.Count == 0);

		if (isMaximizingPlayer == true) {
			foreach (Direction direction in availableMoves) {
				aiGame.PushMove(playerColor.ToInt(), direction);
				float branchval = Minimax(aiGame, depth - 1, alpha, beta, false, playerColor.Flip());
				if (depth == maxDepth && branchval > alpha || depth == maxDepth && bestMove == Direction.None)
					bestMove = direction;
				alpha = Math.Max(alpha, branchval);
				aiGame.PopMove();
				if (beta <= alpha) //beta prune
					break;
			}
			return alpha;
		}
		else {
			foreach (Direction direction in availableMoves) {
				aiGame.PushMove(playerColor.ToInt(), direction);
				beta = Math.Min(beta, Minimax(aiGame, depth - 1, alpha, beta, true, playerColor.Flip()));
				aiGame.PopMove();
				if (beta <= alpha) //alpha prune
					break;
			}
			return beta;
		}
		throw new Exception("minimax tree did not exit properly");
	}
}