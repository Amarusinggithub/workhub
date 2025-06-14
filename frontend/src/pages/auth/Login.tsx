import { useState } from "react";
import InputError from '../../components/input-error';
import TextLink from '../../components/text-link';
import { Button } from '../../components/ui/button';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';


type LoginProps = {
	email: string;
	password: string;
};

const Login = () => {
	const [form, setForm] = useState<LoginProps>({
		email: '',
		password: '',
	});

    const[isLoading,setLoading]=useState<boolean>(false);
    const[error,setError]=useState(null);

	function submit(e) {
		e.preventDefault();
	}

	return (
		<>
			<form className="flex flex-col gap-6" onSubmit={submit}>
				<div className="grid gap-6">
					<div className="grid gap-2">
						<Label htmlFor="email">Email address</Label>
						<Input
							id="email"
							type="email"
							required
							tabIndex={1}
							autoComplete="email"
							value={form.email}
							onChange={(e) => setForm('email', e.target.value)}
							disabled={isLoading}
							placeholder="email@example.com"
						/>
						<InputError message={errors.email} />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="password">Password</Label>
						<Input
							id="password"
							type="password"
							required
							tabIndex={2}
							autoComplete="new-password"
							value={form.password}
							onChange={(e) => setForm('password', e.target.value)}
							disabled={isLoading}
							placeholder="Password"
						/>
						<InputError message={error.password} />
					</div>

					<Button type="submit" className="mt-2 w-full" tabIndex={5} disabled={processing}>
						{isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
						LogIn{' '}
					</Button>
				</div>

				<div className="text-muted-foreground text-center text-sm">
					Dont have an account?{' '}
					<TextLink href={} tabIndex={6}>
						Register{' '}
					</TextLink>
				</div>
			</form>
		</>
	);
};

export default Login;
