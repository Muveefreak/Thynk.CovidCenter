import { RouteInfo } from './sidebar.metadata';

export const ROUTES: RouteInfo[] = [
  {
    path: '',
    title: 'Administrator',
    icon: 'mdi mdi-dots-horizontal',
    class: 'nav-small-cap',
    extralink: true,
    submenu: []
  },
  {
    path: '/administrator/create-user',
    title: 'User',
    icon: 'mdi mdi-account-multiple',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '/administrator/create-location',
    title: 'Location',
    icon: 'mdi mdi-map',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '/administrator/create-date',
    title: 'Available Date',
    icon: 'mdi mdi-calendar',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '',
    title: 'Lab Administrator',
    icon: 'mdi mdi-dots-horizontal',
    class: 'nav-small-cap',
    extralink: true,
    submenu: []
  },
  {
    path: '/labadmin/enter-result',
    title: 'Enter Result',
    icon: 'mdi mdi-pencil-box-outline',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '/labadmin/report',
    title: 'Get Report',
    icon: 'mdi mdi-pencil-box-outline',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '',
    title: 'Individual',
    icon: 'mdi mdi-dots-horizontal',
    class: 'nav-small-cap',
    extralink: true,
    submenu: []
  },
  {
    path: '/individual/book-test',
    title: 'Book Test',
    icon: 'mdi mdi-calendar-clock',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '/individual/cancel-booking',
    title: 'Cancel Booking',
    icon: 'mdi mdi-calendar-clock',
    class: '',
    extralink: false,
    submenu: []
  }
];
