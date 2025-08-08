export interface Actividad {
  id: number;
  nombre: string;
  tipo: 'supervision' | 'administrativa' | 'apoyo'; // o `string` si el backend no devuelve un enum
  active: boolean;
}
