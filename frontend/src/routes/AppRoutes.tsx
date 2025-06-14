import { createBrowserRouter, Navigate, RouterProvider } from 'react-router';
import { useAuth } from './../hooks/useAuth.tsx';

/*const AppLayout = lazy(() => import('../layouts/AppLayout.tsx'));
const Home = lazy(() => import('../pages/Home.tsx'));
const Login = lazy(() => import('../pages/auth/Login.tsx'));
const Register = lazy(() => import('../pages/auth/Register.tsx'));*/

import AppLayout from '../layouts/AppLayout';

import Home from '../pages/Home';

import Login from '../pages/auth/Login';
import Register from '../pages/auth/Register';

const AppRoutes = () => {
	const { isAuthenticated, checkingAuth } = useAuth();
	if (checkingAuth) return null;

	return <RouterProvider router={createBrowserRouter(isAuthenticated ? privateRoutes : publicRoutes)} key={isAuthenticated ? 'auth' : 'guest'} />;
};

export default AppRoutes;

const publicRoutes = [
	{
		path: '/',
		Component: Landing,
	},
	{ path: '/login', Component: Login },
	{ path: '/register', Component: Register },
	{ path: '*', Component: () => <Navigate to="/" replace /> },
];

const privateRoutes = [
	{
		path: '/',
		Component: AppLayout,
		children: [
			{
				index: true,
				Component: Home,
			},

			{ path: '*', Component: () => <Navigate to="/" replace /> },
		],
	},
];
