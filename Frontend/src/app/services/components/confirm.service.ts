import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class ConfirmService {
  async confirm(
    title: string,
    text: string,
    confirmButtonText: string = 'Sí, confirmar',
    icon: SweetAlertIcon = 'warning'
  ): Promise<boolean> {
    const result = await Swal.fire({
      title,
      text,
      icon,
      showCancelButton: true,
      confirmButtonText,
      cancelButtonText: 'Cancelar',
    });

    return result.isConfirmed;
  }
}
