using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomElement.LoadingElement
{
    [UxmlElement]
    public partial class LoadingDotEffect : LoadingEffectElement
    {
        [UxmlAttribute]
        public int NumberOfDot
        {
            get => _numberOfDot;
            set
            {
                _numberOfDot = math.max(value, 0);
                MarkDirtyRepaint();
            }
        }
        private int _numberOfDot;
        
        [UxmlAttribute]
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = math.max(value, 0f);
                MarkDirtyRepaint();
            }
        }
        private float _radius = 5f;

        [UxmlAttribute]
        public float Progress
        {
            get => _progress;
            set
            {
                _progress = math.max(value, 0f);
                MarkDirtyRepaint();
            }
        }
        
        private float _progress;
        
        public LoadingDotEffect()
        {
            generateVisualContent += Draw;
        }

        private void Draw(MeshGenerationContext ctx)
        {
            var height = contentContainer.layout.height;
            var width = contentContainer.layout.width;

            if (_numberOfDot < 2) _numberOfDot = 2;
            
            var painter = ctx.painter2D;

            painter.strokeColor = Color.white;
            painter.fillColor = Color.white;
            
            var dots = new float[_numberOfDot];
            var space = width / (_numberOfDot - 1);
            
            painter.lineWidth = Radius * 2;
            for (var i = 0; i < _numberOfDot; i++)
            {
                painter.BeginPath();

                dots[i] = space * i;
                var currentDot = Mathf.FloorToInt(Progress % _numberOfDot);
                
                if (i == currentDot)
                {
                    var localProgress = Progress - Mathf.Floor(Progress);
                    var sinValue = math.sin(localProgress * math.PI); ;
                    var center = new Vector2(dots[i], height * (1 - sinValue));
                    painter.Arc(center, Radius * (sinValue + 1), 0, 360);
                }
                else
                {
                    var center = new Vector2(dots[i], height);
                    painter.Arc(center, Radius, 0, 360);
                }
                
                painter.Fill();
                painter.ClosePath();
            }
            
        }

        public override void AddProcess(float delta)
        {
            Progress += delta;
        }
    }
}
