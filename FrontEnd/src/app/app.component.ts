import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tarea } from './interfaces/tarea';
import { TareaService } from './service/tarea.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  listaTareas: Tarea[] = [];
  formularioTarea: FormGroup;

  constructor(private _tareaServicio: TareaService, private _fb: FormBuilder) {
    this.formularioTarea = this._fb.group({
      nombre: ['', Validators.required]
    });
  }

  obtenerTareas() {
    this._tareaServicio.getList().subscribe({
      next: (data) => {
        this.listaTareas = data;
      }, error: (e) => { }
    });
  }

  ngOnInit(): void {
    this.obtenerTareas();
  }

  agregarTarea() {
    const request: Tarea = {
      idTarea: 0,
      nombre: this.formularioTarea.value.nombre
    }
    this._tareaServicio.add(request).subscribe({
      next: (data) => {
        this.listaTareas.push(data);
        this.formularioTarea.patchValue({
          nombre: ""
        });
      }, error: (e) => { }
    });
  }

  eliminarTarea(tarea: Tarea) {
    this._tareaServicio.delete(tarea.idTarea).subscribe({
      next: (data) => {
        const nuevaLista = this.listaTareas.filter(item => item.idTarea != tarea.idTarea)
        this.listaTareas = nuevaLista;
      }, error: (e) => { }
    });
  }
}
