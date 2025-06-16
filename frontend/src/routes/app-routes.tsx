import { createBrowserRouter, Navigate, RouterProvider } from 'react-router';
import { useAuth } from '../hooks/use-auth'
import AppLayout from '../layouts/app-layout';
import Login from '../pages/auth/login';
import Register from '../pages/auth/register';
import Welcome from '../pages/welcome';

/*const AppLayout = lazy(() => import('../layouts/AppLayout.tsx'));
const Home = lazy(() => import('../pages/Home.tsx'));
const Login = lazy(() => import('../pages/auth/Login.tsx'));
const Register = lazy(() => import('../pages/auth/Register.tsx'));*/
const AppRoutes = () => {
	const { isAuthenticated, isCheckingAuth } = useAuth();
	if (checkingAuth) return null;

	return <RouterProvider router={createBrowserRouter(isAuthenticated ? privateRoutes : publicRoutes)} key={isAuthenticated ? 'isAuth' : 'notAuth'} />;
};

export default AppRoutes;

const publicRoutes = [
	{
		path: '/',
		Component: Welcome,
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
