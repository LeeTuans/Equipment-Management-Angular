export let listMenuBar = [
  {
    header: 'Dashboard',
    items: [
      {
        icon: `<i class='bx bx-home-circle'></i>`,
        title: 'Home',
        name: 'home',
        link: '/home',
        role: ['User'],
      },
      {
        icon: `<i class='bx bxl-trip-advisor'></i>`,
        title: 'Management',
        name: 'equipment',
        link: '/equipment',
        role: ['Admin'],
      },
      {
        icon: `<i class='bx bxs-book-content' ></i>`,
        title: 'Required',
        name: 'required',
        link: '/required',
        role: ['Admin', 'User'],
      },
      {
        icon: `<i class='bx bxs-user-detail'></i>`,
        title: 'Employee',
        name: 'employee',
        link: '/employee',
        role: ['Admin'],
      },
    ],
  },
  {
    header: 'Account',
    items: [
      {
        icon: `<i class='bx bx-user' ></i>`,
        title: 'Profile',
        name: 'profile',
        link: '/account/profile',
        role: ['Admin', 'User'],
      },
      {
        icon: `<i class='bx bx-lock-open-alt'></i>`,
        title: 'Change password',
        name: 'change-password',
        link: '/account/change-password',
        role: ['Admin', 'User'],
      },
      {
        icon: `<i class='bx bx-log-out-circle'></i>`,
        title: 'Log out',
        name: 'log-out',
        role: ['Admin', 'User'],
      },
    ],
  },
];
