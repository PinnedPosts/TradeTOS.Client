using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTOS.Converters;
using Xamarin.Forms;

namespace TradeTOS.Views
{
    public partial class MetroListView : ListView
    {
        public MetroListView()
        {
            InitializeComponent();
            
        }

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);
            var currentViewCell = content as ViewCell;
            
            if (currentViewCell != null)
            {
                currentViewCell.View.BackgroundColor = ColorTool.GetMetroColor(index);
            }
        }
    }
}
