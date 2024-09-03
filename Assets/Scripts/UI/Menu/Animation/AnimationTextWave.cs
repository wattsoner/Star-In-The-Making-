using TMPro;
using UnityEngine;

public class AnimationTextWave : MonoBehaviour {
    
    [Header("Text Attachables")] 
    [SerializeField] private TMP_Text tmpText;
    
    [Header("Text Movement Settings")] 
    [SerializeField] private float waveSpeed = 2f;
    [SerializeField] private float waveHeight = 5f;

    private void Update() {
        tmpText.ForceMeshUpdate();
        var textInfo = tmpText.textInfo;

        for (var i = 0; i < textInfo.characterCount; i++)
            if (textInfo.characterInfo[i].isVisible) {
                var charInfo = textInfo.characterInfo[i];
                var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (var j = 0; j < 4; j++) {
                    var orig = vertices[charInfo.vertexIndex + j];
                    orig.y += Mathf.Sin(Time.time * waveSpeed + charInfo.index * 0.5f) * waveHeight;
                    vertices[charInfo.vertexIndex + j] = orig;
                }
            }

        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}