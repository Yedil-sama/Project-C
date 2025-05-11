using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Core.Cards
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public ICard Card { get; private set; }

        [SerializeField] private TMP_Text manaCostText;
        [SerializeField] Image icon;

        protected CanvasGroup canvasGroup;
        protected RectTransform rectTransform;
        protected RectTransform canvasRectTransform;
        private Transform originalParent;

        public virtual void Initialize(ICard card)
        {
            Card = card;
            Card.ManaCostChanged += OnManaCostChanged;
            if (card.Icon) icon.sprite = card.Icon;
            OnManaCostChanged(Card.ManaCost);
        }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            Canvas canvas = GetComponentInParent<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            originalParent = transform.parent;
            transform.SetParent(canvasRectTransform, worldPositionStays: false);
            canvasGroup.blocksRaycasts = false;
            (Card as IDraggable)?.OnDragStart();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, null, out Vector2 localMousePos))
            {
                rectTransform.localPosition = localMousePos;
                (Card as IDraggable)?.OnDrag(rectTransform.position);
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(originalParent, worldPositionStays: false);
            canvasGroup.blocksRaycasts = true;
            (Card as IDraggable)?.OnDragEnd(rectTransform.position);
        }

        public virtual void OnPointerEnter(PointerEventData eventData) => (Card as IHighlightable)?.OnHighlight();

        public virtual void OnPointerExit(PointerEventData eventData) => (Card as IHighlightable)?.OnUnhighlight();

        protected virtual void OnDestroy()
        {
            if (Card != null)
            {
                Card.ManaCostChanged -= OnManaCostChanged;
            }
        }

        private void OnManaCostChanged(int value) => manaCostText.text = value.ToString();
    }
}
