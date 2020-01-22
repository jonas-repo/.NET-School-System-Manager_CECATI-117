jQuery(document).ready(function () {
    jQuery("body").on('click', 'button', function () {
        jQuery.noConflict();
        // Si el boton no tiene el atributo ajax no hacemos nada
        if (jQuery(this).data('ajax') === undefined) return;

        // El metodo .data identifica la entrada y la castea al valor más correcto
        if (jQuery(this).data('ajax') !== true) return;

        var form = jQuery(this).closest("form");
        var buttons = jQuery("button", form);
        var button = jQuery(this);
        var url = form.attr('action');

        if (button.data('confirm') !== undefined) {
            if (button.data('confirm') === '') {
                if (!confirm('¿Esta seguro de realizar esta acción?')) return false;
            } else {
                if (!confirm(button.data('confirm'))) return false;
            }
        }

        if (button.data('delete') !== undefined) {
            if (button.data('delete') === true) {
                url = button.data('url');
            }
        } else {
            if (!form.validate()) {
                return false;
            }
        }

        // Creamos un div que bloqueara todo el formulario
        var block = jQuery('<div class="block-loading" />');
        form.prepend(block);

        // En caso de que haya habido un mensaje de alerta
        jQuery(".alert", form).remove();

        // Para los formularios que tengan CKupdate
        if (form.hasClass('CKupdate')) CKupdate();

        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: url,
            success: function (r) {
                block.remove();
                if (r.response) {
                    if (!button.data('reset') !== undefined) {
                        if (button.data('reset')) form.reset();
                    }
                    else {
                        form.find('input:file').val('');
                    }
                }

                // Mostrar mensaje
                if (r.message !== null) {
                    if (r.message.length > 0) {
                        var css = "";
                        if (r.response) css = "alert-success";
                        else css = "alert-danger";

                        var message = '<div class="alert ' + css + ' alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + r.message + '</div>';
                        form.prepend(message);
                    }
                }

                // Ejecutar funciones
                if (r.function !== null) {
                    setTimeout(r.function, 0);
                }
                // Redireccionar
                if (r.href !== null) {
                    if (r.href === 'self') window.location.reload(true);
                    else window.location.href = r.href;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                block.remove();
                form.prepend('<div class="alert alert-warning alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + "Porfavor no dejes vacío ningun campo del registro" + ' | <b>' + "" + '</b></div>');
            }
        });

        return false;
    });
});

jQuery.fn.reset = function () {
    jQuery("input:password,input:file,input:text,textarea", jQuery(this)).val('');
    jQuery("input:checkbox:checked", jQuery(this)).click();
    jQuery("select").each(function () {
        jQuery(this).val(jQuery("option:first", jQuery(this)).val());
    });
};