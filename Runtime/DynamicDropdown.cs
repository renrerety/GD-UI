using Nova;
using NovaSamples.UIControls;
using UnityEngine;
using UnityEngine.Events;

namespace gdui.runtime
{
    public class DynamicDropdown : UIControl<DynamicDropdownVisuals>
    {
        [Tooltip("The event fired when a new item is selected from the dropdown list.")]
        public UnityEvent<int> OnValueChanged = null;

        [SerializeField] [Tooltip("The data used to populate the list of selectable items in the dropdown.")]
        public DropdownData DropdownOptions = new DropdownData();

        /// <summary>
        /// The visuals associated with this dropdown control
        /// </summary>
        private DynamicDropdownVisuals Visuals => View.Visuals as DynamicDropdownVisuals;

        public void Expand()
        {
            // Tell the dropdown to expand, showing a list of
            // selectable options.
            Visuals.Expand(DropdownOptions);
        }

        public void Collapse()
        {
            // Collapse the dropdown and stop tracking it
            // as the expanded focused object.
            Visuals.Collapse();
        }

        private void OnEnable()
        {
            if (View.TryGetVisuals(out DynamicDropdownVisuals visuals))
            {
                // Set default state
                visuals.UpdateVisualState(VisualState.Default);
            }

            // Subscribe to desired events
            View.UIBlock.AddGestureHandler<Gesture.OnHover, DynamicDropdownVisuals>(
                DynamicDropdownVisuals.HandleHovered);
            View.UIBlock.AddGestureHandler<Gesture.OnUnhover, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandleUnhovered);
            View.UIBlock.AddGestureHandler<Gesture.OnPress, DynamicDropdownVisuals>(
                DynamicDropdownVisuals.HandlePressed);
            View.UIBlock.AddGestureHandler<Gesture.OnRelease, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandleReleased);
            View.UIBlock.AddGestureHandler<Gesture.OnCancel, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandlePressCanceled);
            View.UIBlock.AddGestureHandler<Gesture.OnClick, DynamicDropdownVisuals>(HandleDropdownClicked);

            Visuals.OnValueChanged += HandleValueChanged;
            InputManager.OnPostClick += HandlePostClick;

            // Ensure label is initialized
            Visuals.InitSelectionLabel(DropdownOptions.CurrentSelection);
        }

        private void OnDisable()
        {
            // Unsubscribe from events
            View.UIBlock.RemoveGestureHandler<Gesture.OnHover, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandleHovered);
            View.UIBlock.RemoveGestureHandler<Gesture.OnUnhover, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandleUnhovered);
            View.UIBlock.RemoveGestureHandler<Gesture.OnPress, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandlePressed);
            View.UIBlock.RemoveGestureHandler<Gesture.OnRelease, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandleReleased);
            View.UIBlock.RemoveGestureHandler<Gesture.OnCancel, DynamicDropdownVisuals>(DynamicDropdownVisuals
                .HandlePressCanceled);
            View.UIBlock.RemoveGestureHandler<Gesture.OnClick, DynamicDropdownVisuals>(HandleDropdownClicked);

            Visuals.OnValueChanged -= HandleValueChanged;
            InputManager.OnPostClick -= HandlePostClick;
        }

        /// <summary>
        /// Fire the Unity event when the selected value changes.
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void HandleValueChanged(int selectedIndex)
        {
            OnValueChanged?.Invoke(selectedIndex);
        }

        /// <summary>
        /// Handle a <see cref="DropdownVisuals"/> object in the <see cref="ListView">
        /// being clicked, and either expand or collapse it accordingly.
        /// </summary>
        /// <param name="evt">The click event data.</param>
        /// <param name="dropdownControl">The <see cref="ItemVisuals"/> object which was clicked.</param>
        private void HandleDropdownClicked(Gesture.OnClick evt, DynamicDropdownVisuals dropdownControl)
        {
            if (evt.Receiver.transform.IsChildOf(dropdownControl.OptionsView.transform))
            {
                // The clicked object was not the dropdown itself but rather a list item within the dropdown.
                // The dropdownControl itself will handle this event, so we don't need to do anything here.
                return;
            }

            // Toggle the expanded state of the dropdown on click

            if (dropdownControl.IsExpanded)
            {
                Collapse();
            }
            else
            {
                Expand();
            }
        }

        /// <summary>
        /// Handles unfocusing the <see cref="Dropdown"/> if the user clicks somewhere else.
        /// </summary>
        private void HandlePostClick(UIBlock clickedUIBlock)
        {
            if (!Visuals.IsExpanded)
            {
                return;
            }

            if (clickedUIBlock == null || !clickedUIBlock.transform.IsChildOf(transform))
            {
                // Clicked somewhere else, so remove focus.
                Collapse();
            }
        }
    }
}