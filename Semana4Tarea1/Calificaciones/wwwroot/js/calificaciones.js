
document.addEventListener('DOMContentLoaded', function () {
    var selectCurso = document.getElementById('CursoId');
    var creditosElemento = document.getElementById('lblCreditos');
    var estadoElemento = document.getElementById('lblEstado');

    if (!selectCurso || !creditosElemento || !estadoElemento) return;

    function actualizarInformacionCurso() {
        var opcionSeleccionada = selectCurso.options[selectCurso.selectedIndex];
        if (!opcionSeleccionada || !opcionSeleccionada.value) {
            creditosElemento.textContent = '-';
            estadoElemento.textContent = '-';
            return;
        }
        creditosElemento.textContent = opcionSeleccionada.getAttribute('data-creditos') || '-';
        estadoElemento.textContent = opcionSeleccionada.getAttribute('data-estado') || '-';
    }

    selectCurso.addEventListener('change', actualizarInformacionCurso);
    actualizarInformacionCurso();
});
