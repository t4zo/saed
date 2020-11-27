function selectCheckboxAsRadio(id) {
    const checkboxes = document.querySelectorAll(".selectCheckboxAsRadio input[type=checkbox]");

    checkboxes.forEach(checkbox => {
        checkbox.checked = false;
    });

    checkboxes[id].checked = true;
}