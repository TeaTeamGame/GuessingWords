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
        public float PointToNextDotStart
        {
            get => _pointToNextDotStart;
            set
            {
                _pointToNextDotStart = math.clamp(value, 0f, _numberOfDot);
                MarkDirtyRepaint();
            }
        }
        private float _pointToNextDotStart = 0.75f;

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
                
                var startPos = (_progress + _numberOfDot - PointToNextDotStart) % _numberOfDot;
                var endPos = (_progress + PointToNextDotStart) % _numberOfDot;

                if (startPos < endPos && i > startPos && i < endPos)
                {
                    var value =  math.unlerp(startPos, endPos, i);
                    var sinValue = math.sin(value * math.PI);
                    var center = new Vector2(dots[i], height * (1 - sinValue));
                    painter.Arc(center, Radius * (sinValue / 2 + 1), 0, 360);
                }
                else if (startPos > endPos && (i > startPos || i < endPos))
                {
                    var value = i < endPos
                        ? math.unlerp(startPos, endPos + _numberOfDot, i + _numberOfDot)
                        : math.unlerp(startPos, endPos + _numberOfDot, i);
                
                    var sinValue = math.sin(value * math.PI);
                    var center = new Vector2(dots[i], height * (1 - sinValue));
                    painter.Arc(center, Radius * (sinValue / 2 + 1), 0, 360);
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
