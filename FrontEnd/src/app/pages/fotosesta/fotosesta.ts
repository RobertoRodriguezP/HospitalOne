import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, CardModule, ChartModule, ButtonModule],
  templateUrl: './fotosesta.html',
  styleUrls: ['./fotosesta.scss']
})
export class DashboardComponent implements OnInit {
  stats = [
    { label: 'Pacientes Totales', value: '368', icon: 'pi pi-users', trend: '+12%' },
    { label: 'Doctores Disponibles', value: '42', icon: 'pi pi-user-md', trend: '+3' },
    { label: 'Citas Hoy', value: '23', icon: 'pi pi-calendar', trend: '+5' },
    { label: 'Ocupación Consultorios', value: '87%', icon: 'pi pi-chart-line', trend: '+8%' }
  ];

  // Datos para Chart.js (Bar)
  barData: any;
  barOptions: any;

  // Datos para Chart.js (Pie)
  pieData: any;
  pieOptions: any;

  patientDistribution = [
    { name: 'Activos', value: 245 },
    { name: 'Inactivos', value: 89 },
    { name: 'Pendientes', value: 34 }
  ];

  ngOnInit(): void {
    this.barData = {
      labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
      datasets: [
        {
          label: 'Citas',
          data: [65, 78, 92, 88, 102, 115],
          backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--chart-1') || '#42A5F5',
          borderRadius: 8
        },
        {
          label: 'Completadas',
          data: [55, 68, 82, 75, 95, 110],
          backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--chart-2') || '#66BB6A',
          borderRadius: 8
        }
      ]
    };

    this.barOptions = {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: { position: 'top' as const },
        tooltip: {
          backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--card') || '#fff',
          titleColor: getComputedStyle(document.documentElement).getPropertyValue('--foreground') || '#111',
          bodyColor: getComputedStyle(document.documentElement).getPropertyValue('--foreground') || '#111',
          borderColor: getComputedStyle(document.documentElement).getPropertyValue('--border') || '#e6e6e6',
          borderWidth: 1
        }
      },
      scales: {
        x: {
          grid: { display: false, drawBorder: false },
          ticks: { color: getComputedStyle(document.documentElement).getPropertyValue('--muted-foreground') || '#666' }
        },
        y: {
          grid: { color: 'rgba(0,0,0,0.04)' },
          ticks: { color: getComputedStyle(document.documentElement).getPropertyValue('--muted-foreground') || '#666' }
        }
      }
    };

    this.pieData = {
      labels: this.patientDistribution.map(p => p.name),
      datasets: [
        {
          data: this.patientDistribution.map(p => p.value),
          backgroundColor: ['#7c3aed', '#0ea5e9', '#f59e0b']
        }
      ]
    };

    this.pieOptions = {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        tooltip: {
          backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--card') || '#fff',
          titleColor: getComputedStyle(document.documentElement).getPropertyValue('--foreground') || '#111',
          bodyColor: getComputedStyle(document.documentElement).getPropertyValue('--foreground') || '#111',
          borderColor: getComputedStyle(document.documentElement).getPropertyValue('--border') || '#e6e6e6',
          borderWidth: 1
        },
        legend: { display: false }
      }
    };
  }
}
