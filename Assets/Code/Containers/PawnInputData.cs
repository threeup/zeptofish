using System.Collections;

[System.Serializable]
public struct PawnInputData {

	public float axisX;
	public float axisY;
    public bool alphaDown;
    public bool alphaUp;
    public bool bravoDown;
    public bool bravoUp;
    
    public void Reset()
    {
        axisX = 0;
        axisY = 0;
        alphaDown = false;
        alphaUp = false;
        bravoDown = false;
        bravoUp = false;
    }
}
