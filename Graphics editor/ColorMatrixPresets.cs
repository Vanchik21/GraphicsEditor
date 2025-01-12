namespace Graphics_editor
{
    public static class ColorMatrixPresets
    {
        public static readonly float[][] Sepia = {
        new float[] {0.393f, 0.349f, 0.272f, 0, 0},
        new float[] {0.769f, 0.686f, 0.534f, 0, 0},
        new float[] {0.189f, 0.168f, 0.131f, 0, 0},
        new float[] {0, 0, 0, 1, 0},
        new float[] {0, 0, 0, 0, 1}
    };

        public static readonly float[][] Artistic = {
        new float[]{1,0,0,0,0},
        new float[]{0,1,0,0,0},
        new float[]{0,0,1,0,0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 1, 0, 1}
    };

        public static readonly float[][] Grayscale = {
        new float[] {0.299f, 0.299f, 0.299f, 0, 0},
        new float[] {0.587f, 0.587f, 0.587f, 0, 0},
        new float[] {0.114f, 0.114f, 0.114f, 0, 0},
        new float[] {0, 0, 0, 1, 0},
        new float[] {0, 0, 0, 0, 1}
    };

        public static readonly float[][] Spike = {
        new float[]{1+0.3f, 0, 0, 0, 0},
        new float[]{0, 1+0.7f, 0, 0, 0},
        new float[]{0, 0, 1+1.3f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };

        public static readonly float[][] Flash = {new float[]{1+0.9f, 0, 0, 0, 0},
        new float[]{0, 1+1.5f, 0, 0, 0},
        new float[]{0, 0, 1+1.3f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };

        public static readonly float[][] Frozen = {
        new float[]{1+0.3f, 0, 0, 0, 0},
        new float[]{0, 1+0f, 0, 0, 0},
        new float[]{0, 0, 1+5f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };

        public static readonly float[][] Suji = {
        new float[]{.393f, .349f+0.5f, .272f, 0, 0},
        new float[]{.769f+0.3f, .686f, .534f, 0, 0},
        new float[]{.189f, .168f, .131f+0.5f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };

        public static readonly float[][] Dramatic = {
        new float[]{.393f+0.3f, .349f, .272f, 0, 0},
        new float[]{.769f, .686f+0.2f, .534f, 0, 0},
        new float[]{.189f, .168f, .131f+0.9f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };

        public static readonly float[][] Kakao = {
        new float[]{.393f, .349f, .272f+1.3f, 0, 0},
        new float[]{.769f, .686f+0.5f, .534f, 0, 0},
        new float[]{.189f+2.3f, .168f, .131f, 0, 0},
        new float[]{0, 0, 0, 1, 0},
        new float[]{0, 0, 0, 0, 1}
    };
    }
}