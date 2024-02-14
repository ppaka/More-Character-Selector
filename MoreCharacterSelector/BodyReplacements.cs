using System;
using System.Collections;
using ModelReplacement;
using UnityEngine;

namespace MoreCharacterSelector
{
    public class MRAKULA : BodyReplacementBase
    {
        public bool Blink = true;

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
            StartCoroutine(OnIdle());
        }

        protected override void OnEmoteStart(int emoteId)
        {
            var meshRenderer = replacementModel.GetComponentInChildren<SkinnedMeshRenderer>();
            meshRenderer.SetBlendShapeWeight(7, 0);
            meshRenderer.SetBlendShapeWeight(62, 0);
            meshRenderer.SetBlendShapeWeight(76, 0);

            meshRenderer.SetBlendShapeWeight(5, 0);
            meshRenderer.SetBlendShapeWeight(42, 0);
            meshRenderer.SetBlendShapeWeight(91, 0);

            if (emoteId == 1)
            {
                meshRenderer.SetBlendShapeWeight(5, 100);
                meshRenderer.SetBlendShapeWeight(42, 100);
                meshRenderer.SetBlendShapeWeight(91, (float)87.5);
            }
            if (emoteId == 2)
            {
                meshRenderer.SetBlendShapeWeight(7, 100);
                meshRenderer.SetBlendShapeWeight(62, 26);
                meshRenderer.SetBlendShapeWeight(76, 100);
            }
            Blink = false;
        }

        protected override void OnEmoteEnd()
        {
            var meshRenderer = replacementModel.GetComponentInChildren<SkinnedMeshRenderer>();
            meshRenderer.SetBlendShapeWeight(7, 0);
            meshRenderer.SetBlendShapeWeight(62, 0);
            meshRenderer.SetBlendShapeWeight(76, 0);

            meshRenderer.SetBlendShapeWeight(5, 0);
            meshRenderer.SetBlendShapeWeight(42, 0);
            meshRenderer.SetBlendShapeWeight(91, 0);
            Blink = true;
        }

        protected override void OnDeath()
        {
            Blink = true;

            foreach (var r in replacementDeadBody.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var blinkIndex = r.sharedMesh.GetBlendShapeIndex("EyeClose");
                if (blinkIndex != -1)
                {
                    r.SetBlendShapeWeight(blinkIndex, 100f);
                    Blink = false;
                }
            }
        }
        /*protected override void OnDeath()
        {
            foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var smileIndex = r.sharedMesh.GetBlendShapeIndex("Smile");
                var surpriseIndex = r.sharedMesh.GetBlendShapeIndex("Surprise");
                if (smileIndex != -1) {
                    r.SetBlendShapeWeight(smileIndex, 100f);
                }
                if (surpriseIndex != -1) {
                    r.SetBlendShapeWeight(surpriseIndex, 100f);
                }
            }
        }*/

        IEnumerator OnIdle()
        {
            while (true)
            {
                if (Blink == true)
                {
                    foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        var blinkIndex = r.sharedMesh.GetBlendShapeIndex("EyeClose");
                        if (blinkIndex != -1)
                        {
                            r.SetBlendShapeWeight(blinkIndex, 100f);
                        }
                    }

                    yield return new WaitForSeconds(0.2f);

                    foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        var blinkIndex = r.sharedMesh.GetBlendShapeIndex("EyeClose");
                        if (blinkIndex != -1)
                        {
                            r.SetBlendShapeWeight(blinkIndex, 0f);
                        }
                    }

                    yield return new WaitForSeconds(15f);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }

    public class MRPEPE : BodyReplacementBase
    {
        public bool Blink = true;

        protected override GameObject LoadAssetsAndReturnModel()
        {
            string model_name = "pepe";
            return Assets.MainAssetBundle.LoadAsset<GameObject>(model_name);
        }

        protected override void AddModelScripts()
        {
            UseNoPostProcessing = true;
            StartCoroutine(OnIdle());
        }

        protected override void OnEmoteStart(int emoteId)
        {
            foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var lockIndex = r.sharedMesh.GetBlendShapeIndex("Lock");
                var sadIndex = r.sharedMesh.GetBlendShapeIndex("Sad");
                if (lockIndex != -1)
                {
                    if (emoteId == 1)
                    {
                        r.SetBlendShapeWeight(lockIndex, 100f);
                    }
                    else
                    {
                        r.SetBlendShapeWeight(lockIndex, 0f);
                    }
                }
                if (sadIndex != -1)
                {
                    if (emoteId == 2)
                    {
                        r.SetBlendShapeWeight(sadIndex, 100f);
                    }
                    else
                    {
                        r.SetBlendShapeWeight(sadIndex, 0f);
                    }
                }
            }
            Blink = false;
        }

        protected override void OnEmoteEnd()
        {
            foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var lockIndex = r.sharedMesh.GetBlendShapeIndex("Lock");
                var sadIndex = r.sharedMesh.GetBlendShapeIndex("Sad");
                if (lockIndex != -1)
                {
                    r.SetBlendShapeWeight(lockIndex, 0f);
                }
                if (sadIndex != -1)
                {
                    r.SetBlendShapeWeight(sadIndex, 0f);
                }
            }
            Blink = true;
        }

        protected override void OnDeath()
        {
            Blink = true;

            foreach (var r in replacementDeadBody.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var blinkIndex = r.sharedMesh.GetBlendShapeIndex("CloseEye");
                if (blinkIndex != -1)
                {
                    r.SetBlendShapeWeight(blinkIndex, 100f);
                    Blink = false;
                }
            }
        }

        IEnumerator OnIdle()
        {
            while (true)
            {
                if (Blink == true)
                {
                    foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        var blinkIndex = r.sharedMesh.GetBlendShapeIndex("CloseEye");
                        if (blinkIndex != -1)
                        {
                            r.SetBlendShapeWeight(blinkIndex, 100f);
                        }
                    }

                    yield return new WaitForSeconds(0.3f);

                    foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        var blinkIndex = r.sharedMesh.GetBlendShapeIndex("CloseEye");
                        if (blinkIndex != -1)
                        {
                            r.SetBlendShapeWeight(blinkIndex, 0f);
                        }
                    }

                    yield return new WaitForSeconds(15f);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}