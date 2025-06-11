using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPieceFactory : MonoBehaviour
{

    // not really a factory :P doesn't create the pieces at runtime but whatevs couln't think of a better name at the time

    public MusicPiece[] MusicPieces;

    public MusicPieceFactory(MusicPiece[] musicPieces) {
        MusicPieces = musicPieces;
    }

    public MusicPiece GetMusicPiece(string name) {
        foreach (MusicPiece musicPiece in MusicPieces) {
            if (musicPiece.Name == name) {
                return musicPiece;
            }
        } return null;
    }

}
