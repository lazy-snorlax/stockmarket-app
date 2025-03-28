import { SyntheticEvent, useState } from "react"
import CardList from "../components/CardList"
import Search from "../components/Search"
import { CompanySearch } from "../company"
import { searchCompanies } from "../api"

const SearchPage = () => {
    const [search, setSearch] = useState<string>("")
    const [searchResult, setSearchResult] = useState<CompanySearch[]>([])
    const [serverError, setServerError] = useState<string | null>(null)

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
    };

    const onPortfolioCreate = (e: SyntheticEvent) => {
        e.preventDefault();
        console.log(">>> creating portfolio", e)
    };

    const onSearchSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        console.log(">>> searching for: ", search)
        const result = await searchCompanies(search)
        if (typeof result === "string") {
            setServerError(result)
        } else if (Array.isArray(result.data)) {
            setSearchResult(result.data)
        }
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