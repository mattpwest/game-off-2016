using System.Collections;

public class Colour {
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }
    public float a { get; set; }

    public Colour(float r, float g, float b) : this(r, g, b, 1.0f) {
    }

    public Colour(float r, float g, float b, float a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
}
