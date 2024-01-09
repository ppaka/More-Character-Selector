using ModelReplacement;
using UnityEngine;

namespace MoreCharacterSelector
{
    public class MRAKULA : BodyReplacementBase
    {
        //Required universally
        protected override GameObject LoadAssetsAndReturnModel()
        { 
            string model_name = "Akula";
            return Assets.MainAssetBundle.LoadAsset<GameObject>(model_name);
        }
        
        //Miku mod specific scripts. Delete this if you have no custom scripts to add. 
        protected override void AddModelScripts()
        {
            UseNoPostProcessing = true;
            //InvokeRepeating("OnIdle", 0f, 15f);
        }
        
        
        protected override void OnEmoteStart(int emoteId)
        {
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(7, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(62, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(76, 0);

            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(5, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(42, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(91, 0);

            if (emoteId == 1)
            {
                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(5, 100);
                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(42, 100);
                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(91, (float)87.5);
            }
            if(emoteId == 2)
            {
               

                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(7, 100);
                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(62, 26);
                replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(76, 100);
            }
            
        }

        protected override void OnEmoteEnd()
        {
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(7, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(62, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(76, 0);

            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(5, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(42, 0);
            replacementModel.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(91, 0);
        }

        protected override void OnDeath()
        {
            foreach (var r in replacementDeadBody.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var blinkIndex = r.sharedMesh.GetBlendShapeIndex("EyeClose");
                if (blinkIndex != -1)
                {
                    r.SetBlendShapeWeight(blinkIndex, 100f);
                }
            }
        }
        
        /*// ReSharper disable Unity.PerformanceAnalysis
        private void OnIdle()
        {
            foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var blinkIndex = r.sharedMesh.GetBlendShapeIndex("EyeClose");
                if (blinkIndex != -1)
                {
                    r.SetBlendShapeWeight(blinkIndex, 100f);
                }
            }
        }*/
        
    }
    public class MRAKULA_ : BodyReplacementBase
    {
        protected override GameObject LoadAssetsAndReturnModel()
        { 
            string model_name = "Akula";
            return Assets.MainAssetBundle.LoadAsset<GameObject>(model_name);
        }
    }
}
