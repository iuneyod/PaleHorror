using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FnafHouseManager : MonoBehaviour
{
    public TMP_Text progressText;
    public float Progress = 0;
    public float MaxProgress = 100;
    public float ProgressSpeed = 1f;
    public float DegressSpeed = 0.1f;
    public bool estaSubiendo = false;

    Coroutine progresoCoroutine;
    Coroutine subirProgresoCoroutine;
    Coroutine bajarProgresoCoroutine;

    public GameObject Player;
    public GameObject cameraRoot;
    public Transform PlayerSpawnInvernadero;
    Animator playerAnimator;

    Teddy teddy;
    StarterAssetsInputs inputs;
    ThirdPersonController controller;

    public bool Derrota = false;
    public bool victoria = false;
    bool juegoBloqueado = false;


    public GameObject skyBoxOriginal;
    public GameObject skyBoxFnaf;

    public GameObject player;
    public GameObject geometry;
    public Transform playerTransform;
    public GameObject cameraPlayer;
    public Transform cameraTransform;

    Coroutine rotateCoroutine;
    public float rotateDuration = 0.08f;

    bool yaAparecio = false;

    public bool mirandoAlFrente = false;
    public bool mirandoALaIzquierda = false;
    public bool mirandoALaDerecha = false;
    public bool mirandoAtras = false;
    bool aperecerNumerosDetras = false;
    bool bienHecho = false;

    enum MonsterPoseIzq
    {
        PrimeraIZQ,
        SegundaIZQ,
        TerceraIZQ,
        CuartaIZQ
    }

    enum MonsterPoseDer
    {
        PrimeraDER,
        SegundaDER,
        TerceraDER,
        CuartaDER
    }

    MonsterPoseIzq poseActualIZQ = MonsterPoseIzq.PrimeraIZQ;
    MonsterPoseDer poseActualDER = MonsterPoseDer.PrimeraDER;


    //monster izquierda ventana
    public GameObject linterna;
    bool linternaEncendida = false;
    bool puedeUsarLinterna = false;
    public GameObject ventanaIZQ;

    public Transform PrimeraPose_Hide;
    public Transform SegundaPose;
    public Transform TerceraPose_Ventana;
    public Transform CuartaPose_Delante;
    public GameObject monsterIzquierdaPose1;
    public GameObject monsterIzquierdaPose2;
    public GameObject monsterIzquierdaPose3;
    public GameObject monsterIzquierdaPose4;

    public GameObject monsterIzquierdaSusto;
    bool sustoIzquierdaEnCurso = false;
    bool monsterIzquierdaBloqueado = false;


    //monster detras
    public GameObject monsterDetrasSusto;
    public GameObject posterDetras;
    public GameObject Numero1;
    public GameObject Numero2;
    public GameObject Numero3;

    bool posterBloqueado = false;
    bool posterActivo = false;
    bool numerosActivos = false;
    bool esperandoRespuesta = false;
    bool posterInteractuado = false;
    bool sustoAtrasEnCurso = false;

    Coroutine esperarPosterCoroutine;
    Coroutine esperarNumerosCoroutine;



    //monster Derecha
    public GameObject monsterDerechaSusto;

    public GameObject monsterDerecha_Pose1;
    public GameObject monsterDerecha_Pose2;
    public GameObject monsterDerecha_Pose3;
    public GameObject monsterDerecha_PoseUltima;

    bool monsterDerechaBloqueado = false;

    bool ALERTA = false;







    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        teddy = FindObjectOfType<Teddy>();
        inputs = player.GetComponent<StarterAssetsInputs>();
        controller = player.GetComponent<ThirdPersonController>();
        puedeUsarLinterna = true;
        mirandoAlFrente = true;
    }

    void Update()
    {
        if (juegoBloqueado)
            return;

        progressText.text = Mathf.RoundToInt(Progress) + "%";

        if (teddy.activatedTeddyTrap && !yaAparecio)
        {
            progressText.gameObject.SetActive(true);
            yaAparecio = true;
            FreezeTrap();
            StartCoroutine(AparecerPoster());
            skyBoxOriginal.SetActive(false);
            skyBoxFnaf.SetActive(true);
            MonsterIzquierda();
            MonsterDerecha();

        }

        if (!yaAparecio) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateCameraSmooth(90f);
            mirandoALaIzquierda = true;
            mirandoAlFrente = false;
            mirandoAtras = false;
            mirandoALaDerecha = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateCameraSmooth(180f);
            mirandoAlFrente = true;
            mirandoALaIzquierda = false;
            mirandoALaDerecha = false;
            mirandoAtras = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateCameraSmooth(270f);
            mirandoALaDerecha = true;
            mirandoAlFrente = false;
            mirandoALaIzquierda = false;
            mirandoAtras = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RotateCameraSmooth(0f);
            mirandoAtras = true;
            mirandoAlFrente = false;
            mirandoALaIzquierda = false;
            mirandoALaDerecha = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Lintern());
        }

        if (linternaEncendida == true && poseActualIZQ == MonsterPoseIzq.TerceraIZQ && mirandoALaIzquierda == true)
        {
            ActivarPose(MonsterPoseIzq.PrimeraIZQ);
        }

        if (poseActualIZQ == MonsterPoseIzq.CuartaIZQ && sustoIzquierdaEnCurso == false)
        {
            sustoIzquierdaEnCurso = true;
            StartCoroutine(AparecerMonsterIzquierdaSusto());
        }

        if (mirandoAtras == true && posterDetras.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            posterInteractuado = true;
            aperecerNumerosDetras = true;
            AparecerNumeros();
        }


        if (esperandoRespuesta)
        {
            if (Numero1.activeSelf && Input.GetKeyDown(KeyCode.Alpha1))
            {
                ResolverPoster();
                posterActivo = false;
            }

            else if (Numero2.activeSelf && Input.GetKeyDown(KeyCode.Alpha2))
            {
                ResolverPoster();
                posterActivo = false;
            }
            else if (Numero3.activeSelf && Input.GetKeyDown(KeyCode.Alpha3))
            {
                ResolverPoster();
                posterActivo = false;
            }
        }

        if (poseActualDER == MonsterPoseDer.CuartaDER && Input.GetKeyDown(KeyCode.E) && mirandoAlFrente == true)
        {
            StartCoroutine(ALERTA_SIMULACRO());
        }

        if (poseActualDER == MonsterPoseDer.CuartaDER && mirandoAlFrente == true && linternaEncendida == true)
        {
            StartCoroutine(AparecerMonsterDerechaSusto());
        }


        bool puedeSubir = mirandoAlFrente && Input.GetKey(KeyCode.R);

        if (puedeSubir)
        {
            //parar bajada
            if (bajarProgresoCoroutine != null)
            {
                StopCoroutine(bajarProgresoCoroutine);
                bajarProgresoCoroutine = null;
            }

            // iniciar subida
            if (subirProgresoCoroutine == null)
                subirProgresoCoroutine = StartCoroutine(SubirProgreso());
        }
        else
        {
            //parar subida
            if (subirProgresoCoroutine != null)
            {
                StopCoroutine(subirProgresoCoroutine);
                subirProgresoCoroutine = null;
            }

            // iniciar bajada
            if (bajarProgresoCoroutine == null)
                bajarProgresoCoroutine = StartCoroutine(BajarProgreso());
        }



        if (Progress <= 0)
        {
            Progress = 0;
        }
        if (Progress >= MaxProgress)
        {
            Progress = MaxProgress;
            DegressSpeed = 0;
        }

        if (Progress >= MaxProgress && !victoria)
        {
            victoria = true;
            juegoBloqueado = true;

            StopAllCoroutines(); //PARA TODO FNAF
            StartCoroutine(VictoriaCorutina());
        }

        //if(Derrota)
        //{
        //    victoria = false;
        //    juegoBloqueado = true;
        //    StopAllCoroutines(); //PARA TODO FNAF
        //    StartCoroutine(DerrotaCorutina());
        //}

    }

    IEnumerator VictoriaCorutina()
    {
        yield return new WaitForSeconds(1.5f);

        teddy.activatedTeddyTrap = false;

        player.transform.position = PlayerSpawnInvernadero.position;

        controller.inputBloqueado = false;

        // reset inputs manualmente
        inputs.move = Vector2.zero;
        inputs.look = Vector2.zero;
        inputs.jump = false;
        inputs.sprint = false;

        cameraPlayer.transform.parent = cameraRoot.transform;
        cameraPlayer.transform.localPosition = new Vector3(0f, 0.315f, 0f);
        cameraPlayer.transform.localRotation = Quaternion.identity;

        geometry.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        Animator anim = geometry.GetComponentInChildren<Animator>();
        anim.Rebind();
        anim.Update(0f);

        skyBoxFnaf.SetActive(false);
        skyBoxOriginal.SetActive(true);
    }
    IEnumerator DerrotaCorutina()
    {
        yield return new WaitForSeconds(1.5f);

        teddy.activatedTeddyTrap = false;

        player.transform.position = PlayerSpawnInvernadero.position;

        controller.inputBloqueado = false;

        // reset inputs manualmente
        inputs.move = Vector2.zero;
        inputs.look = Vector2.zero;
        inputs.jump = false;
        inputs.sprint = false;

        cameraPlayer.transform.parent = cameraRoot.transform;
        cameraPlayer.transform.localPosition = new Vector3(0f, 0.315f, 0f);
        cameraPlayer.transform.localRotation = Quaternion.identity;

        geometry.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        Animator anim = geometry.GetComponentInChildren<Animator>();
        anim.Rebind();
        anim.Update(0f);

        skyBoxFnaf.SetActive(false);
        skyBoxOriginal.SetActive(true);
    }




    IEnumerator SubirProgreso()
    {
        while (Progress < MaxProgress && !Derrota)
        {
            Progress += 1f;
            yield return new WaitForSeconds(0.3f);
        }

        subirProgresoCoroutine = null;
    }


    IEnumerator BajarProgreso()
    {
        while (Progress > 0 && !Derrota)
        {
            Progress -= 0.1f;
            yield return new WaitForSeconds(0.3f);
        }

        bajarProgresoCoroutine = null;
    }




    void ProgresoSubir(int cantidad)
    {
        Progress += cantidad;
        Progress = Mathf.Clamp(Progress, 0, MaxProgress);
    }
    void ProgresoBajar(float cantidad)
    {
        Progress -= cantidad;
        Progress = Mathf.Clamp(Progress, 0, MaxProgress);
    }



    IEnumerator AparecerMonsterIzquierdaSusto()
    {
        if (Derrota) yield break;

        Derrota = true;
        monsterIzquierdaBloqueado = true;

        yield return new WaitForSeconds(4f);

        monsterIzquierdaSusto.SetActive(true);
        monsterIzquierdaPose4.SetActive(false);

        StartCoroutine(CameraShake(1.2f, 0.1f));
        StartCoroutine(MonsterIZQShake(1.2f, 0.05f));


        yield return new WaitForSeconds(1.2f);

        monsterIzquierdaSusto.SetActive(false);

        // FORZAR que se quede en la pose 1
        ActivarPose(MonsterPoseIzq.PrimeraIZQ);
    }

    IEnumerator AparecerMonsterDerechaSusto()
    {
        if (Derrota) yield break;

        Derrota = true;

        monsterDerechaSusto.SetActive(true);
        monsterDerecha_PoseUltima.SetActive(false);

        StartCoroutine(CameraShake(1.2f, 0.1f));
        StartCoroutine(MonsterDERShake(1.2f, 0.05f));


        yield return new WaitForSeconds(1.2f);

        monsterDerechaSusto.SetActive(false);

        // FORZAR que se quede en la pose 1
        ActivarPoseDER(MonsterPoseDer.PrimeraDER);
    }

    IEnumerator AparecerMonsterAtrasSusto()
    {
        if (Derrota || sustoAtrasEnCurso) yield break;

        sustoAtrasEnCurso = true;
        Derrota = true;

        monsterDetrasSusto.SetActive(true);
        monsterDerecha_PoseUltima.SetActive(false);

        StartCoroutine(CameraShake(1.2f, 0.1f));
        StartCoroutine(MonsterAtrasShake(1.2f, 0.05f));

        yield return new WaitForSeconds(1.2f);

        monsterDetrasSusto.SetActive(false);
    }


    IEnumerator ALERTA_SIMULACRO()
    {
        Progress -= 5f;
        ActivarPoseDER(MonsterPoseDer.PrimeraDER);
        ALERTA = true;
        yield return new WaitForSeconds(3f);
        ALERTA = false;
    }

    public void MonsterDerecha()
    {
        ActivarPoseDER(MonsterPoseDer.PrimeraDER);

        StartCoroutine(MoverMonsterDER());
    }

    IEnumerator MoverMonsterDER()
    {
        while (true)
        {
            if (monsterDerechaBloqueado)
                yield break;

            yield return new WaitForSeconds(1f);

            int chance = Random.Range(0, 3);

            if (chance != 0)
                continue;

            AvanzarPoseDerecha();
        }
    }

    void AvanzarPoseDerecha()
    {
        if (monsterDerechaBloqueado)
            return;

        switch (poseActualDER)
        {
            case MonsterPoseDer.PrimeraDER:
                ActivarPoseDER(MonsterPoseDer.SegundaDER);
                break;
            case MonsterPoseDer.SegundaDER:
                ActivarPoseDER(MonsterPoseDer.TerceraDER);
                break;
            case MonsterPoseDer.TerceraDER:
                ActivarPoseDER(MonsterPoseDer.CuartaDER);
                break;
        }
    }

    void ActivarPoseDER(MonsterPoseDer nuevaPose)
    {
        if (monsterDerechaBloqueado && nuevaPose != MonsterPoseDer.PrimeraDER)
            return;

        // Apagar todas las poses DERECHA
        monsterDerecha_Pose1.SetActive(false);
        monsterDerecha_Pose2.SetActive(false);
        monsterDerecha_Pose3.SetActive(false);
        monsterDerecha_PoseUltima.SetActive(false);

        poseActualDER = nuevaPose;

        switch (poseActualDER)
        {
            case MonsterPoseDer.PrimeraDER:
                monsterDerecha_Pose1.SetActive(true);
                break;

            case MonsterPoseDer.SegundaDER:
                monsterDerecha_Pose2.SetActive(true);
                break;

            case MonsterPoseDer.TerceraDER:
                monsterDerecha_Pose3.SetActive(true);
                break;

            case MonsterPoseDer.CuartaDER:
                monsterDerecha_PoseUltima.SetActive(true);
                break;
        }

    }




    IEnumerator Lintern()
    {
        linterna.SetActive(true);
        linternaEncendida = true;
        yield return new WaitForSeconds(0.3f);
        linterna.SetActive(false);
        linternaEncendida = false;
    }


    void RotateCameraSmooth(float targetY)
    {
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(RotateCameraCoroutine(
            Quaternion.Euler(0f, targetY, 0f)
        ));
    }

    IEnumerator RotateCameraCoroutine(Quaternion targetRotation)
    {
        Quaternion startRotation = cameraPlayer.transform.rotation;
        float elapsed = 0f;

        while (elapsed < rotateDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / rotateDuration;

            cameraPlayer.transform.rotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                t
            );

            yield return null;
        }

        cameraPlayer.transform.rotation = targetRotation;
    }


    void FreezeTrap()
    {
        cameraPlayer.transform.parent = null;
        cameraPlayer.transform.position = cameraTransform.position;
        cameraPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);

        geometry.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        controller.inputBloqueado = true;
    }


    public void MonsterIzquierda()
    {
        ActivarPose(MonsterPoseIzq.PrimeraIZQ);

        StartCoroutine(MonsterIzquierdaSecuencia());
    }


    IEnumerator MonsterIzquierdaSecuencia()
    {
        while (true)
        {
            if (monsterIzquierdaBloqueado)
                yield break;

            yield return new WaitForSeconds(4f);

            int chance = Random.Range(0, 3);

            if (chance != 0)
                continue;

            AvanzarPoseIzquierda();
        }
    }

    



    void AvanzarPoseIzquierda()
    {
        if (monsterIzquierdaBloqueado)
            return;

        switch (poseActualIZQ)
        {
            case MonsterPoseIzq.PrimeraIZQ:
                ActivarPose(MonsterPoseIzq.SegundaIZQ);
                break;
            case MonsterPoseIzq.SegundaIZQ:
                ActivarPose(MonsterPoseIzq.TerceraIZQ);
                break;
            case MonsterPoseIzq.TerceraIZQ:
                ActivarPose(MonsterPoseIzq.CuartaIZQ);
                break;
        }
    }



    void ActivarPose(MonsterPoseIzq nuevaPose)
    {
        if (monsterIzquierdaBloqueado && nuevaPose != MonsterPoseIzq.PrimeraIZQ)
            return;

        // Apagar todas
        monsterIzquierdaPose1.SetActive(false);
        monsterIzquierdaPose2.SetActive(false);
        monsterIzquierdaPose3.SetActive(false);
        monsterIzquierdaPose4.SetActive(false);

        poseActualIZQ = nuevaPose;

        switch (poseActualIZQ)
        {
            case MonsterPoseIzq.PrimeraIZQ:
                monsterIzquierdaPose1.SetActive(true);
                break;

            case MonsterPoseIzq.SegundaIZQ:
                monsterIzquierdaPose2.SetActive(true);
                break;

            case MonsterPoseIzq.TerceraIZQ:
                monsterIzquierdaPose3.SetActive(true);
                break;

            case MonsterPoseIzq.CuartaIZQ:
                monsterIzquierdaPose4.SetActive(true);
                break;
        }
    }


    void Poster()
    {
        posterActivo = true;
        posterInteractuado = false;
        bienHecho = false;

        if (esperarPosterCoroutine != null)
            StopCoroutine(esperarPosterCoroutine);

        if (esperarNumerosCoroutine != null)
            StopCoroutine(esperarNumerosCoroutine);

        posterDetras.SetActive(true);
        esperarPosterCoroutine = StartCoroutine(EsperarPoster());
    }




    IEnumerator AparecerPoster()
    {
        while (true)
        {
            if (posterBloqueado)
                yield break;

            // esperar entre intentos
            yield return new WaitForSeconds(4.5f);

            // si ya hay un poster activo, no intentes sacar otro
            if (posterActivo || numerosActivos)
                continue;

            int chance = Random.Range(0, 3);

            if (chance != 0)
                continue;

            Poster();
        }
    }


    void AparecerNumeros()
    {
        numerosActivos = true;
        esperandoRespuesta = true;

        posterDetras.SetActive(false);

        Numero1.SetActive(false);
        Numero2.SetActive(false);
        Numero3.SetActive(false);

        int numero = Random.Range(1, 4);

        if (numero == 1) Numero1.SetActive(true);
        if (numero == 2) Numero2.SetActive(true);
        if (numero == 3) Numero3.SetActive(true);

        StartCoroutine(EsperarNumeros());
    }

    void ResolverPoster()
    {
        bienHecho = true;
        esperandoRespuesta = false;
        numerosActivos = false;
        posterActivo = false;

        if (esperarPosterCoroutine != null)
            StopCoroutine(esperarPosterCoroutine);

        if (esperarNumerosCoroutine != null)
            StopCoroutine(esperarNumerosCoroutine);

        Numero1.SetActive(false);
        Numero2.SetActive(false);
        Numero3.SetActive(false);
    }




    IEnumerator EsperarPoster()
    {
        yield return new WaitForSeconds(7f);

        if (!posterActivo) yield break;
        if (posterInteractuado) yield break;
        if (Derrota) yield break;

        posterActivo = false;
        posterDetras.SetActive(false);
        numerosActivos = false;

        StartCoroutine(AparecerMonsterAtrasSusto());
    }



    IEnumerator EsperarNumeros()
    {
        yield return new WaitForSeconds(2.2f);

        if (!numerosActivos) yield break;
        if (bienHecho || Derrota) yield break;

        StartCoroutine(AparecerMonsterAtrasSusto());
    }



    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraPlayer.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraPlayer.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraPlayer.transform.localPosition = originalPos;  // restaurar posición
    }
    private IEnumerator MonsterIZQShake(float duration, float magnitude)
    {
        Vector3 originalPos = monsterIzquierdaSusto.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            monsterIzquierdaSusto.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        monsterIzquierdaSusto.transform.localPosition = originalPos;  // restaurar posición
    }

    private IEnumerator MonsterDERShake(float duration, float magnitude)
    {
        Vector3 originalPos = monsterDerechaSusto.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            monsterDerechaSusto.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        monsterDerechaSusto.transform.localPosition = originalPos;  // restaurar posición
    }

    private IEnumerator MonsterAtrasShake(float duration, float magnitude)
    {
        Vector3 originalPos = monsterDetrasSusto.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            monsterDetrasSusto.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        monsterDetrasSusto.transform.localPosition = originalPos;  // restaurar posición
    }





}
