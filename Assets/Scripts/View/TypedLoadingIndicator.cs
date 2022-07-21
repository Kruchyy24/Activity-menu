using System;
using UnityEngine;
using UnityEngine.Serialization;
using View.Types;

namespace View
{
    [Serializable]
    public class TypedLoadingIndicator
    {
        [FormerlySerializedAs("_loadingIndicatorView")] [SerializeField] private LoadingIndicatorView loadingIndicatorView;
        [FormerlySerializedAs("_loadingIndicatorType")] [SerializeField] private LoadingIndicatorType loadingIndicatorType;

        public LoadingIndicatorType IndicatorType => loadingIndicatorType;

        public LoadingIndicatorView IndicatorView => loadingIndicatorView;
    }
}