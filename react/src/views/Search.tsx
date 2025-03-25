import { useState } from "react"
import CardList from "../components/CardList"
import Search from "../components/Search"

const SearchPage = () => {
    const [search, setSearch] = useState<string>("")
    const [searchResult, setSearchResult] = useState([])
    const [serverError, setServerError] = useState<string | null>(null)

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
    };

    const onPortfolioCreate = (e: any) => {
        e.preventDefault();
        console.log(">>> creating portfolio")
    };

    const onSearchSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        console.log(">>> searching for: ", search)
        setSearchResult([])
    };

    return (
        <>
            <Search 
                onSearchSubmit={onSearchSubmit} 
                search={search}
                handleSearchChange={handleSearchChange}
            />
            <CardList 
                searchResults={searchResult} 
                onPortfolioCreate={onPortfolioCreate} 
            />
            {serverError && <div>Unable to connect to API</div>}
        </>
    )
}

export default SearchPage