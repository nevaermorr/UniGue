using System;
public class IntVector2 {
	
	public int x;
	public int y;

	public IntVector2 (int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public IntVector2 Shift(int x, int y) {
		return new IntVector2 (this.x + x, this.y + y);
	}
}


