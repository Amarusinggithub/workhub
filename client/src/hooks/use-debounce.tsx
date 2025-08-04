import { useEffect, useState } from 'react';

function useDebounce<T>(value: T, delay: number) {
	const [debounceValue, setDebounceValue] = useState<T>();

	useEffect(() => {
		const timeOut = setTimeout(() => {
			setDebounceValue(value);
		}, delay);

		return () => {
			clearTimeout(timeOut);
		};
	}, []);

	return debounceValue;
}

export default useDebounce;
