using System;
using System.Collections;
using ModelReplacement;
using UnityEngine;

namespace MoreCharacterSelector
{
    public static class BaseSkinnedMeshComponentFinder
    {
        public static SkinnedMeshRenderer GetBaseSkinnedMeshRenderer(string modelName, string bodyObjectName, GameObject replacementModel)
        {
            Console.WriteLine($"[MoreCharacterSelector] [Model:{modelName}] Trying Get Body from GetComponentInChildren()");
            var meshRenderer = replacementModel.GetComponentInChildren<SkinnedMeshRenderer>();
            if (meshRenderer.name != bodyObjectName)
            {
                Console.WriteLine($"[MoreCharacterSelector] [Model:{modelName}] Failed!");
                Console.WriteLine($"[MoreCharacterSelector] [Model:{modelName}] Trying Get Body from Components Array...");
                foreach (var r in replacementModel.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    if (r.name == bodyObjectName)
                    {
                        meshRenderer = r;
                        break;
                    }
                }
            }

            if (meshRenderer.name != bodyObjectName)
            {
                Console.WriteLine($"[MoreCharacterSelector] [Model:{modelName}] Getting Body Failed!");
            }

            return meshRenderer;
        }
    }

    public class MRAKULA : BodyReplacementBase
    {
        private const string BODY_COMPONENT_OBJECT_NAME = "Body";
        private const string MODEL_NAME = "Akula";

        public bool Blink = true;
        private SkinnedMeshRenderer meshRenderer;
        private WaitForSeconds eyeCloseWait, eyeOpenWait;
        private int blinkIndex;

        //Required universally
        protected override GameObject LoadAssetsAndReturnModel()
        {
            return Assets.MainAssetBundle.LoadAsset<GameObject>(MODEL_NAME);
        }

        protected override void Start()
        {
            base.Start();
            eyeCloseWait = new WaitForSeconds(0.2f);
            eyeOpenWait = new WaitForSeconds(15f);

            meshRenderer = BaseSkinnedMeshComponentFinder.GetBaseSkinnedMeshRenderer(MODEL_NAME, BODY_COMPONENT_OBJECT_NAME, replacementModel);            
            blinkIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("EyeClose");
            Console.WriteLine($"[MoreCharacterSelector] [Model:{MODEL_NAME}] Initialized Successfully!");

            StartCoroutine(OnIdle());
        }

        //Miku mod specific scripts. Delete this if you have no custom scripts to add. 
        protected override void AddModelScripts()
        {
            UseNoPostProcessing = true;
        }

        protected override void OnEmoteStart(int emoteId)
        {
            if (!meshRenderer)
            {
                return;
            }

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
                meshRenderer.SetBlendShapeWeight(91, 87.5f);
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
            if (!meshRenderer)
            {
                return;
            }

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
            if (!meshRenderer)
            {
                return;
            }

            meshRenderer.SetBlendShapeWeight(blinkIndex, 100);
            Blink = false;
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
            if (!meshRenderer)
            {
                yield break;
            }

            while (true)
            {
                if (Blink == true)
                {
                    meshRenderer.SetBlendShapeWeight(blinkIndex, 100);
                    yield return eyeCloseWait;

                    meshRenderer.SetBlendShapeWeight(blinkIndex, 0);
                    yield return eyeOpenWait;
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
        private const string BODY_COMPONENT_OBJECT_NAME = "Plane";
        private const string MODEL_NAME = "pepe";

        public bool Blink = true;
        private SkinnedMeshRenderer meshRenderer;
        private WaitForSeconds eyeCloseWait, eyeOpenWait;
        private int blinkIndex, lockIndex, sadIndex;

        protected override GameObject LoadAssetsAndReturnModel()
        {
            return Assets.MainAssetBundle.LoadAsset<GameObject>(MODEL_NAME);
        }

        protected override void Start()
        {
            base.Start();
            eyeCloseWait = new WaitForSeconds(0.3f);
            eyeOpenWait = new WaitForSeconds(15f);

            meshRenderer = BaseSkinnedMeshComponentFinder.GetBaseSkinnedMeshRenderer(MODEL_NAME, BODY_COMPONENT_OBJECT_NAME, replacementModel);
            blinkIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("EyeClose");
            lockIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Lock");
            sadIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Sad");
            if (blinkIndex == -1)
            {
                Console.WriteLine($"[MoreCharacterSelector] [Model:{MODEL_NAME}] Cannot Find [EyeClose] Index");
            }
            if (lockIndex == -1)
            {
                Console.WriteLine($"[MoreCharacterSelector] [Model:{MODEL_NAME}] Cannot Find [Lock] Index");
            }
            if (sadIndex == -1)
            {
                Console.WriteLine($"[MoreCharacterSelector] [Model:{MODEL_NAME}] Cannot Find [Sad] Index");
            }
            Console.WriteLine($"[MoreCharacterSelector] [Model:{MODEL_NAME}] Initialized Successfully!");

            StartCoroutine(OnIdle());
        }

        protected override void AddModelScripts()
        {
            UseNoPostProcessing = true;
        }

        protected override void OnEmoteStart(int emoteId)
        {
            if (!meshRenderer)
            {
                return;
            }

            if (emoteId == 1)
            {
                meshRenderer.SetBlendShapeWeight(lockIndex, 100);
            }
            else
            {
                meshRenderer.SetBlendShapeWeight(lockIndex, 0);
            }

            if (emoteId == 2)
            {
                meshRenderer.SetBlendShapeWeight(sadIndex, 100);
            }
            else
            {
                meshRenderer.SetBlendShapeWeight(sadIndex, 0);
            }

            Blink = false;
        }

        protected override void OnEmoteEnd()
        {
            if (!meshRenderer)
            {
                return;
            }

            meshRenderer.SetBlendShapeWeight(lockIndex, 0);
            meshRenderer.SetBlendShapeWeight(sadIndex, 0);
            Blink = true;
        }

        protected override void OnDeath()
        {
            if (!meshRenderer)
            {
                return;
            }

            meshRenderer.SetBlendShapeWeight(blinkIndex, 100);
            Blink = false;
        }

        IEnumerator OnIdle()
        {
            if (!meshRenderer)
            {
                yield break;
            }

            while (true)
            {
                if (Blink == true)
                {
                    meshRenderer.SetBlendShapeWeight(blinkIndex, 100);
                    yield return eyeCloseWait;

                    meshRenderer.SetBlendShapeWeight(blinkIndex, 0);
                    yield return eyeOpenWait;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}