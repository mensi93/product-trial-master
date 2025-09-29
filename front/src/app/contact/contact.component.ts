import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,DialogModule, ButtonModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {

  private readonly fb = inject(FormBuilder);

  showDialog = false;
  dialogMessage = '';

  contactForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    message: ['', [Validators.required, Validators.maxLength(300)]]
  });

  submit() {
    if (this.contactForm.valid) {
      this.dialogMessage = 'Merci pour votre message !';
      this.showDialog = true;
      this.contactForm.reset();
    } else {
      this.dialogMessage = 'Veuillez remplir correctement tous les champs.';
      this.showDialog = true;
    }
  }
  
  closeDialog() {
    this.showDialog = false;
  }


}
