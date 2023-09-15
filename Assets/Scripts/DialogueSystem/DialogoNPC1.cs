namespace RedBlueGames.Tools.TextTyper
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using RedBlueGames.Tools.TextTyper;
    using UnityEngine.UI;
    using TMPro;

    public class DialogoNPC1 : MonoBehaviour
    {
        /*--TABLA-DE-REFERENCIAS-DE-COMENTARIOS-//

            LNK = ~LNK

        //--TABLA-DE-REFERENCIAS-DE-COMENTARIOS-*/

        /// ESTE SCRIPT NO ESTÁ REWORKEADO PARA PENDULUM, cualquier duda preguntar a LNK. ~LNK

        #pragma warning disable 0649 // Ignore "Field is never assigned to" warning, as these are assigned in inspector
        
        [Header("Referencias de audio")]
        [SerializeField] private AudioClip printSoundEffect;



        [Header("Ref de UI | botones de acciones")]
        [SerializeField] private Button printNextButton;



        //lineas de dialogo introductorios. LNK~
        private Queue<string> dialogueLines = new Queue<string>();

        [SerializeField]string dialogo1;
        [SerializeField]string dialogo2;
        [SerializeField]string dialogo3;
        [SerializeField]string dialogo4;

        

        [SerializeField]
        [Tooltip("TextTyper script")]
        private TextTyper testTextTyper;





#pragma warning restore 0649
        public void Awake()
        {


            this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
            this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

            this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);


            //aqui se añade y configura el boton de las respuestas. LNK~
            
            // Lista de efectos. LNK~
            // aplicar delay: <delay=0.5>NPC</delay>
            // aplicar italica: <i>bub</i>
            // aplicar negrita: <b>use</b>
            // cambiar tamaño de letra: <size=40>text</size>
            // aplicar color: <color=#ff0000ff>color</color>
            // efecto delay: <delay=0.5>PALABRA</delay>
            // efecto shake de rotacion ligera: <anim=lightrot>Light Rotation</anim>
            // efecto shake de posicion ligera: <anim=lightpos>Light Position</anim>
            // efecto shake completo: <anim=fullshake>Full Shake</anim>
            // efecto de curva: <animation=slowsine>Slow Sine</animation>
            // efecto de rebote: <animation=bounce>Bounce Bounce</animation>
            // efecto de flip (muy loco): <animation=crazyflip>Crazy Flip</animation>
            // para aplicar punto y aparte, utilizar: \n


            // Aqui se crean las lineas de dialogos, cada una con su respectiva respuesta. LNK~

            dialogueLines.Enqueue(dialogo1);
            dialogueLines.Enqueue(dialogo2);
            dialogueLines.Enqueue(dialogo3);
            dialogueLines.Enqueue(dialogo4);
            //dialogueLines.Enqueue(dialogo);
            
           
            ShowScript();
        }

        public void Update()
        {

            //UnityEngine.Time.timeScale = this.pauseGameToggle.isOn ? 0.0f : 1.0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                var tag = RichTextTag.ParseNext("blah<color=red>boo</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("<color=blue>blue</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No tag in here");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No <color=blueblue</color tag here either");
                LogTag(tag);
                tag = RichTextTag.ParseNext("This tag is a closing tag </bold>");
                LogTag(tag);
            }


        }

        private void HandlePrintNextClicked()
        {
            if (this.testTextTyper.IsSkippable() && this.testTextTyper.IsTyping)
            {
                this.testTextTyper.Skip();
            }
            else
            {
                ShowScript();
            }
        }

        private void HandlePrintNoSkipClicked()
        {
            ShowScript();
        }

        private void ShowScript()
        {
            if (dialogueLines.Count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            this.testTextTyper.TypeText(dialogueLines.Dequeue());
        }

        private void LogTag(RichTextTag tag)
        {
            if (tag != null)
            {
                Debug.Log("Tag: " + tag.ToString());
            }
        }

        private void HandleCharacterPrinted(string printedCharacter)
        {
            // Esta sentencia es para que no se reproduzca un sonido cuando hay silencio o cambio de linea. LNK~
            if (printedCharacter == " " || printedCharacter == "\n")
            {
                return;
            }

            // Aqui se llama al AudioSource para realizar sonidos con efecto de habla. LNK~
            var audioSource = this.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = this.gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = this.printSoundEffect;
            audioSource.Play();
        }

        private void HandlePrintCompleted()
        {
            Debug.Log("TypeText Complete");
        }


        



    }

}
