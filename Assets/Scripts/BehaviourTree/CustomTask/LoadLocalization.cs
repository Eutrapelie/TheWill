using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace TheWill
{
    [Category("♥ The Will")]
    public class LoadLocalization : ActionTask
    {
        public BBParameter<int> minPathIndex;
        public BBParameter<int> maxPathIndex;

        protected override string info
        {
            get
            {
                return string.Format("Load localization between TABLE_REFERENCE_LOCALISATION_" + minPathIndex.value.ToString("D2") + "\n and TABLE_REFERENCE_LOCALISATION_" + maxPathIndex.value.ToString("D2"));
            }
        }

        protected override void OnExecute()
        {
            Debug.Log("<color=blue>[LoadLocalization] Load between: " + minPathIndex.value + " and " + maxPathIndex.value + "</color>");

            Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang(), minPathIndex.value, maxPathIndex.value);

            EndAction(true);
        }
    }
}
